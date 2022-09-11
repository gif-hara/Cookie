using System;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GachaSceneController : SceneController
    {
        [SerializeField]
        private Transform uiParent;

        [SerializeField]
        private GachaUIView gachaUIPrefab;

        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var uiView = Instantiate(this.gachaUIPrefab, this.uiParent);
            
            uiView.WeaponGachaButton.Button.onClick.AddListener(() =>
            {
                uiView.DestroyAllGachaButtons();
                foreach (var gacha in MasterDataWeaponGacha.Instance.gachas)
                {
                    
                }
            });

            GlobalMessagePipe.GetSubscriber<GachaEvent.RequestWeaponGacha>()
                .Subscribe(x =>
                {
                    var weaponGacha = MasterDataWeaponGacha.Instance.gachas.Find(x.WeaponGachaId);
                    var newWeapon = new Weapon
                    {
                        instanceId = UserData.current.weaponCreatedNumber,
                        nameKey = "Test",
                        physicalStrength = Random.Range(weaponGacha.physicalStrengthMin, weaponGacha.physicalStrengthMax),
                        magicStrength = Random.Range(weaponGacha.magicStrengthMin, weaponGacha.magicStrengthMax)
                    };
                    var skillNumber = Random.Range(weaponGacha.skillNumberMin, weaponGacha.skillNumberMax + 1);
                    for (var i = 0; i < skillNumber; i++)
                    {
                        newWeapon.activeSkillIds.Add(weaponGacha.activeSkillIds.Lottery().value);
                    }
                    UserData.current.weapons.Add(newWeapon);
                    UserData.current.weaponCreatedNumber++;
                    SaveData.SaveUserData(UserData.current);
                    Debug.Log(JsonUtility.ToJson(newWeapon, true));
                })
                .AddTo(scope);
            
            GlobalMessagePipe.GetSubscriber<GachaEvent.RequestArmorGacha>()
                .Subscribe(x =>
                {
                    var armorGacha = MasterDataArmorGacha.Instance.gachas.Find(x.ArmorGachaId);
                    var newArmor = new Armor
                    {
                        instanceId = UserData.current.armorCreatedNumber,
                        nameKey = "Test",
                        hitPoint = Random.Range(armorGacha.hitPointMin, armorGacha.hitPointMax),
                        physicalDefense = Random.Range(armorGacha.physicalDefenseMin, armorGacha.physicalDefenseMax),
                        magicDefense = Random.Range(armorGacha.magicDefenseMin, armorGacha.magicDefenseMax),
                        speed = Random.Range(armorGacha.speedMin, armorGacha.speedMax)
                    };
                    UserData.current.armors.Add(newArmor);
                    UserData.current.armorCreatedNumber++;
                    SaveData.SaveUserData(UserData.current);
                    Debug.Log(JsonUtility.ToJson(newArmor, true));
                })
                .AddTo(scope);
            
            GlobalMessagePipe.GetSubscriber<GachaEvent.RequestAccessoryGacha>()
                .Subscribe(x =>
                {
                    var accessoryGacha = MasterDataAccessoryGacha.Instance.gachas.Find(x.AccessoryGachaId);
                    var newAccessory = new Accessory
                    {
                        instanceId = UserData.current.accessoryCreatedNumber,
                        nameKey = "Test"
                    };
                    var skillNumber = Random.Range(accessoryGacha.skillNumberMin, accessoryGacha.skillNumberMax + 1);
                    for (var i = 0; i < skillNumber; i++)
                    {
                        newAccessory.passiveSkillIds.Add(accessoryGacha.passiveSkillIds.Lottery().value);
                    }
                    UserData.current.accessories.Add(newAccessory);
                    UserData.current.accessoryCreatedNumber++;
                    SaveData.SaveUserData(UserData.current);
                    Debug.Log(JsonUtility.ToJson(newAccessory, true));
                })
                .AddTo(scope);

            HeaderUIViewUtility.Setup(uiView.HeaderUIView);
            
            return base.OnStartAsync(scope);
        }
        
        protected override void OnInitializeMessageBroker(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<GachaEvent.RequestWeaponGacha>();
            builder.AddMessageBroker<GachaEvent.RequestArmorGacha>();
            builder.AddMessageBroker<GachaEvent.RequestAccessoryGacha>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log(JsonUtility.ToJson(UserData.current, true));
            }
        }
    }
}
