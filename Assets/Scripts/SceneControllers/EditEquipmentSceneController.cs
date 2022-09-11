using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentSceneController : SceneController
    {
        [SerializeField]
        private Transform uiParent;

        [SerializeField]
        private EditEquipmentUIView editEquipmentUIViewPrefab;

        private CookieButton selectedRootButton;

        private CookieButton selectedEquipmentButton;

        private Weapon selectedWeapon;

        private Armor selectedArmor;

        private Accessory selectedAccessory;
        
        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var uiView = Instantiate(this.editEquipmentUIViewPrefab, this.uiParent);

            void CreateWeaponList()
            {
                this.SetSelectedRootButton(uiView.WeaponButton);
                uiView.DestroyAllEquipmentButtons();
                this.SetSelectedEquipmentButton(null);
                uiView.ConfirmRoot.SetActive(false);
                foreach (var weapon in UserData.current.weapons)
                {
                    var button = uiView.CreateEquipmentButton();
                    button.Message.text = UserData.current.equippedWeaponInstanceId == weapon.instanceId
                        ? $"[E] {weapon.Name}"
                        : weapon.Name;
                    button.Button.onClick.AddListener(() =>
                    {
                        this.SetSelectedEquipmentButton(button);
                        uiView.ConfirmRoot.SetActive(true);
                        this.selectedWeapon = weapon;
                        this.selectedArmor = null;
                        this.selectedAccessory = null;
                        var equippedWeapon = UserData.current.EquippedWeapon;
                        uiView.EquipmentInformationUIView.Setup(equippedWeapon, weapon);
                    });
                }
            }

            void CreateArmorList()
            {
                this.SetSelectedRootButton(uiView.ArmorButton);
                uiView.DestroyAllEquipmentButtons();
                this.SetSelectedEquipmentButton(null);
                uiView.ConfirmRoot.SetActive(false);
                foreach (var armor in UserData.current.armors)
                {
                    var button = uiView.CreateEquipmentButton();
                    button.Message.text = UserData.current.equippedArmorInstanceId == armor.instanceId
                        ? $"[E] {armor.Name}"
                        : armor.Name;
                    button.Button.onClick.AddListener(() =>
                    {
                        this.SetSelectedEquipmentButton(button);
                        uiView.ConfirmRoot.SetActive(true);
                        this.selectedWeapon = null;
                        this.selectedArmor = armor;
                        this.selectedAccessory = null;
                        var equippedArmor = UserData.current.EquippedArmor;
                        uiView.EquipmentInformationUIView.Setup(equippedArmor, armor);
                    });
                }
            }

            void CreateAccessoryList()
            {
                this.SetSelectedRootButton(uiView.AccessoryButton);
                uiView.DestroyAllEquipmentButtons();
                this.SetSelectedEquipmentButton(null);
                uiView.ConfirmRoot.SetActive(false);
                foreach (var accessory in UserData.current.accessories)
                {
                    var button = uiView.CreateEquipmentButton();
                    button.Message.text = UserData.current.equippedAccessoryInstanceId == accessory.instanceId
                        ? $"[E] {accessory.Name}"
                        : accessory.Name;
                    button.Button.onClick.AddListener(() =>
                    {
                        this.SetSelectedEquipmentButton(button);
                        uiView.ConfirmRoot.SetActive(true);
                        this.selectedWeapon = null;
                        this.selectedArmor = null;
                        this.selectedAccessory = accessory;
                        var equippedAccessory = UserData.current.EquippedAccessory;
                        uiView.EquipmentInformationUIView.Setup(equippedAccessory, accessory);
                    });
                }
            }
            
            uiView.WeaponButton.Button.onClick.AddListener(() =>
            {
                CreateWeaponList();
                uiView.EquipmentInformationUIView.SetDeactiveAll();
            });
            
            uiView.ArmorButton.Button.onClick.AddListener(() =>
            {
                CreateArmorList();
                uiView.EquipmentInformationUIView.SetDeactiveAll();
            });
            
            uiView.AccessoryButton.Button.onClick.AddListener(() =>
            {
                CreateAccessoryList();
                uiView.EquipmentInformationUIView.SetDeactiveAll();
            });
            
            uiView.EquipmentButton.Button.onClick.AddListener(() =>
            {
                if (this.selectedWeapon != null)
                {
                    UserData.current.equippedWeaponInstanceId = this.selectedWeapon.instanceId;
                    SaveData.SaveUserData(UserData.current);
                    CreateWeaponList();
                    var equippedWeapon = UserData.current.EquippedWeapon;
                    uiView.EquipmentInformationUIView.Setup(equippedWeapon, equippedWeapon);
                }
                if (this.selectedArmor != null)
                {
                    UserData.current.equippedArmorInstanceId = this.selectedArmor.instanceId;
                    SaveData.SaveUserData(UserData.current);
                    CreateArmorList();
                    var equippedArmor = UserData.current.EquippedArmor;
                    uiView.EquipmentInformationUIView.Setup(equippedArmor, equippedArmor);
                }
                if (this.selectedAccessory != null)
                {
                    UserData.current.equippedAccessoryInstanceId = this.selectedAccessory.instanceId;
                    SaveData.SaveUserData(UserData.current);
                    CreateAccessoryList();
                    var equippedAccessory = UserData.current.EquippedAccessory;
                    uiView.EquipmentInformationUIView.Setup(equippedAccessory, equippedAccessory);
                }
            });

            HeaderUIViewUtility.Setup(uiView.HeaderUIView);
            
            uiView.ConfirmRoot.SetActive(false);
            uiView.EquipmentInformationUIView.SetDeactiveAll();
            
            return base.OnStartAsync(scope);
        }
        
        protected override void OnInitializeMessageBroker(BuiltinContainerBuilder builder)
        {
        }

        private void SetSelectedRootButton(CookieButton equipmentButton)
        {
            if (this.selectedRootButton != null)
            {
                this.selectedRootButton.PositiveStylist.Apply();
            }

            this.selectedRootButton = equipmentButton;
            
            if (this.selectedRootButton != null)
            {
                this.selectedRootButton.SelectedStylist.Apply();
            }
        }

        private void SetSelectedEquipmentButton(CookieButton equipmentButton)
        {
            if (this.selectedEquipmentButton != null)
            {
                this.selectedEquipmentButton.PositiveStylist.Apply();
            }

            this.selectedEquipmentButton = equipmentButton;
            
            if (this.selectedEquipmentButton != null)
            {
                this.selectedEquipmentButton.SelectedStylist.Apply();
            }
        }
    }
}
