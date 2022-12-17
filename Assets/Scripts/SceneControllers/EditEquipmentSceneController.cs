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
        private EditEquipmentUIView editEquipmentUIViewPrefab;

        private CookieButton selectedRootButton;

        private CookieButton selectedEquipmentButton;

        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var uiView = UIManager.Open(this.editEquipmentUIViewPrefab);

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
                        uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedWeapon, weapon);
                        
                        // 装備ボタンが押されたら装備する
                        uiView.EquipmentButton.Button.onClick.RemoveAllListeners();
                        uiView.EquipmentButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.equippedWeaponInstanceId = weapon.instanceId;
                            SaveData.SaveUserData(UserData.current);
                            CreateWeaponList();
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedWeapon, UserData.current.EquippedWeapon);
                        });
                        
                        // 破棄ボタンが押されたら削除する
                        uiView.DiscardButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardButton.Button.onClick.AddListener(() =>
                        {
                            // 装備中の武器は削除出来ない
                            if (UserData.current.equippedWeaponInstanceId == weapon.instanceId)
                            {
                                // TODO: UIで表示したほうが良いかも
                                return;
                            }
                            UserData.current.weapons.Remove(weapon);
                            SaveData.SaveUserData(UserData.current);
                            CreateWeaponList();
                            uiView.EquipmentInformationUIView.SetDeactiveAll();
                        });
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
                        uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedArmor, armor);
                                                
                        // 装備ボタンが押されたら装備する
                        uiView.EquipmentButton.Button.onClick.RemoveAllListeners();
                        uiView.EquipmentButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.equippedArmorInstanceId = armor.instanceId;
                            SaveData.SaveUserData(UserData.current);
                            CreateArmorList();
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedArmor, UserData.current.EquippedArmor);
                        });
                                                
                        // 破棄ボタンが押されたら削除する
                        uiView.DiscardButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardButton.Button.onClick.AddListener(() =>
                        {
                            // 装備中の武器は削除出来ない
                            if (UserData.current.equippedArmorInstanceId == armor.instanceId)
                            {
                                // TODO: UIで表示したほうが良いかも
                                return;
                            }
                            UserData.current.armors.Remove(armor);
                            SaveData.SaveUserData(UserData.current);
                            CreateArmorList();
                            uiView.EquipmentInformationUIView.SetDeactiveAll();
                        });
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
                        uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedAccessory, accessory);
                                                                        
                        // 装備ボタンが押されたら装備する
                        uiView.EquipmentButton.Button.onClick.RemoveAllListeners();
                        uiView.EquipmentButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.equippedAccessoryInstanceId = accessory.instanceId;
                            SaveData.SaveUserData(UserData.current);
                            CreateAccessoryList();
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedAccessory, UserData.current.EquippedAccessory);
                        });
                                                
                        // 破棄ボタンが押されたら削除する
                        uiView.DiscardButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardButton.Button.onClick.AddListener(() =>
                        {
                            // 装備中の武器は削除出来ない
                            if (UserData.current.equippedAccessoryInstanceId == accessory.instanceId)
                            {
                                // TODO: UIで表示したほうが良いかも
                                return;
                            }
                            UserData.current.accessories.Remove(accessory);
                            SaveData.SaveUserData(UserData.current);
                            CreateAccessoryList();
                            uiView.EquipmentInformationUIView.SetDeactiveAll();
                        });
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
            
            HeaderUIViewUtility.Setup(uiView.HeaderUIView, scope);
            
            uiView.ConfirmRoot.SetActive(false);
            uiView.EquipmentInformationUIView.SetDeactiveAll();

            MessageBroker.Scene.GetSubscriber<SceneEvent.OnDestroy>()
                .Subscribe(_ =>
                {
                    UIManager.Close(uiView);
                })
                .AddTo(scope);
            
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
