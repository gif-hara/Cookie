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

        private CookieButton selectedRootButton;

        private CookieButton selectedGachaButton;

        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var uiView = Instantiate(this.gachaUIPrefab, this.uiParent);
            
            uiView.WeaponGachaButton.Button.onClick.AddListener(() =>
            {
                this.SetSelectedRootButton(uiView.WeaponGachaButton);
                uiView.DestroyAllGachaButtons();
                uiView.ConfirmListRoot.SetActive(false);
                foreach (var gacha in MasterDataWeaponGacha.Instance.gachas)
                {
                    var gachaButton = uiView.CreateGachaButton();
                    gachaButton.Message.text = gacha.Name;
                    gachaButton.Button.onClick.AddListener(() =>
                    {
                        this.SetSelectedGachaButton(gachaButton);
                        uiView.ConfirmListRoot.SetActive(true);
                        uiView.InvokeButton.Button.onClick.RemoveAllListeners();
                        uiView.InvokeButton.Button.onClick.AddListener(() =>
                        {
                            var newWeapon = new Weapon
                            {
                                instanceId = UserData.current.weaponCreatedNumber,
                                nameKey = "Test",
                                physicalStrength = Random.Range(gacha.physicalStrengthMin, gacha.physicalStrengthMax),
                                magicStrength = Random.Range(gacha.magicStrengthMin, gacha.magicStrengthMax)
                            };
                            var skillNumber = Random.Range(gacha.skillNumberMin, gacha.skillNumberMax + 1);
                            for (var i = 0; i < skillNumber; i++)
                            {
                                newWeapon.activeSkillIds.Add(gacha.activeSkillIds.Lottery().value);
                            }
                            UserData.current.weapons.Add(newWeapon);
                            UserData.current.weaponCreatedNumber++;
                            SaveData.SaveUserData(UserData.current);
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedWeapon, newWeapon);
                        });
                    });
                }
            });
            
            uiView.ArmorGachaButton.Button.onClick.AddListener(() =>
            {
                this.SetSelectedRootButton(uiView.ArmorGachaButton);
                uiView.DestroyAllGachaButtons();
                uiView.ConfirmListRoot.SetActive(false);
                foreach (var gacha in MasterDataArmorGacha.Instance.gachas)
                {
                    var gachaButton = uiView.CreateGachaButton();
                    gachaButton.Message.text = gacha.Name;
                    gachaButton.Button.onClick.AddListener(() =>
                    {
                        this.SetSelectedGachaButton(gachaButton);
                        uiView.ConfirmListRoot.SetActive(true);
                        uiView.InvokeButton.Button.onClick.RemoveAllListeners();
                        uiView.InvokeButton.Button.onClick.AddListener(() =>
                        {
                            var newArmor = new Armor
                            {
                                instanceId = UserData.current.armorCreatedNumber,
                                nameKey = "Test",
                                hitPoint = Random.Range(gacha.hitPointMin, gacha.hitPointMax),
                                physicalDefense = Random.Range(gacha.physicalDefenseMin, gacha.physicalDefenseMax),
                                magicDefense = Random.Range(gacha.magicDefenseMin, gacha.magicDefenseMax),
                                speed = Random.Range(gacha.speedMin, gacha.speedMax)
                            };
                            UserData.current.armors.Add(newArmor);
                            UserData.current.armorCreatedNumber++;
                            SaveData.SaveUserData(UserData.current);
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedArmor, newArmor);
                        });
                    });
                }
            });
            
            uiView.AccessoryGachaButton.Button.onClick.AddListener(() =>
            {
                this.SetSelectedRootButton(uiView.AccessoryGachaButton);
                uiView.DestroyAllGachaButtons();
                uiView.ConfirmListRoot.SetActive(false);
                foreach (var gacha in MasterDataAccessoryGacha.Instance.gachas)
                {
                    var gachaButton = uiView.CreateGachaButton();
                    gachaButton.Message.text = gacha.Name;
                    gachaButton.Button.onClick.AddListener(() =>
                    {
                        this.SetSelectedGachaButton(gachaButton);
                        uiView.ConfirmListRoot.SetActive(true);
                        uiView.InvokeButton.Button.onClick.RemoveAllListeners();
                        uiView.InvokeButton.Button.onClick.AddListener(() =>
                        {
                            var newAccessory = new Accessory
                            {
                                instanceId = UserData.current.accessoryCreatedNumber,
                                nameKey = "Test"
                            };
                            var skillNumber = Random.Range(gacha.skillNumberMin, gacha.skillNumberMax + 1);
                            for (var i = 0; i < skillNumber; i++)
                            {
                                newAccessory.passiveSkillIds.Add(gacha.passiveSkillIds.Lottery().value);
                            }
                            UserData.current.accessories.Add(newAccessory);
                            UserData.current.accessoryCreatedNumber++;
                            SaveData.SaveUserData(UserData.current);
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedAccessory, newAccessory);
                        });
                    });
                }
            });
            
            uiView.ConfirmListRoot.SetActive(false);
            uiView.EquipmentInformationUIView.SetDeactiveAll();
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

        private void SetSelectedRootButton(CookieButton rootButton)
        {
            if (this.selectedRootButton != null)
            {
                this.selectedRootButton.PositiveStylist.Apply();
            }

            this.selectedRootButton = rootButton;

            if (this.selectedRootButton != null)
            {
                this.selectedRootButton.SelectedStylist.Apply();
            }
        }

        private void SetSelectedGachaButton(CookieButton gachaButton)
        {
            if (this.selectedGachaButton != null)
            {
                this.selectedGachaButton.PositiveStylist.Apply();
            }

            this.selectedGachaButton = gachaButton;

            if (this.selectedGachaButton != null)
            {
                this.selectedGachaButton.SelectedStylist.Apply();
            }
        }
    }
}
