using System;
using System.Linq;
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
        private GachaUIView gachaUIPrefab;

        private CookieButton selectedRootButton;

        private CookieButton selectedGachaButton;

        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var userData = UserData.current;
            var uiView = UIManager.Open(this.gachaUIPrefab);
            
            uiView.DestroyAllGachaButtons();
            foreach (var gachaId in UserData.current.unlockWeaponGachas)
            {
                var gacha = MasterDataWeaponGacha.Instance.gachas.Find(x => x.id == gachaId);
                var gachaButton = uiView.CreateGachaButton();
                gachaButton.NewIcon.SetActive(!userData.weaponGachaInvokeCounts.ContainsKey(gachaId));
                gachaButton.Message.text = gacha.Name;
                gachaButton.Button.onClick.AddListener(() =>
                {
                    this.SetSelectedGachaButton(gachaButton);
                    uiView.InvokeListRoot.SetActive(true);
                    uiView.ConfirmListRoot.SetActive(false);
                    uiView.EquipmentInformationUIView.SetDeactiveAll();
                    uiView.InvokeButton.Message.text = string.Format(
                        LocalizeString.Get("UI", "InvokeMoneyFormat"),
                        gacha.money
                        );
                    
                    // 実行ボタンが押されたらガチャする
                    uiView.InvokeButton.Button.onClick.RemoveAllListeners();
                    uiView.InvokeButton.Button.onClick.AddListener(() =>
                    {
                        if (!UserData.current.IsPossessionMoney(gacha.money))
                        {
                            UIManager.NotifyUIController.Show(LocalizeString.Get("UI", "NotEnoughMoney")).Forget();
                            return;
                        }
                        userData.AddMoney(-gacha.money);
                        if (!userData.weaponGachaInvokeCounts.ContainsKey(gachaId))
                        {
                            userData.weaponGachaInvokeCounts.Add(gachaId, 0);
                        }
                        userData.weaponGachaInvokeCounts[gachaId]++;
                        gachaButton.NewIcon.SetActive(false);

                        var physicalStrength = gacha.physicalStrengths.Lottery();
                        var magicStrength = gacha.magicStrengths.Lottery();
                        var criticalRate = gacha.criticalRates.Lottery();
                        var newWeapon = new Weapon
                        {
                            instanceId = UserData.current.weaponCreatedNumber,
                            nameKey = "Test",
                            physicalStrength = new InstanceParameter
                            {
                                parameter = physicalStrength.value.GetParameter(),
                                rare = physicalStrength.value.rare
                            },
                            magicStrength = new InstanceParameter
                            {
                                parameter = magicStrength.value.GetParameter(),
                                rare = magicStrength.value.rare
                            },
                            criticalRate = new InstanceParameter
                            {
                                parameter = criticalRate.value.GetParameter(),
                                rare = criticalRate.value.rare
                            }
                        };
                        var skillNumber = gacha.skillNumbers.Lottery().value.GetParameter();
                        for (var i = 0; i < skillNumber; i++)
                        {
                            var instanceParameter = gacha.activeSkillIds.Lottery().value;
                            var activeSkill = MasterDataActiveSkill.Instance.skills.Find(x => x.id == instanceParameter.parameter);
                            var attachNumber = newWeapon.activeSkillIds
                                .Count(x => x.parameter == instanceParameter.parameter);
                            
                            // アタッチ可能数を超えていた場合は付与できない
                            if (activeSkill.attachMax <= attachNumber)
                            {
                                // もう一度スキルを抽選する
                                i--;
                                continue;
                            }
                            newWeapon.activeSkillIds.Add(new InstanceParameter(instanceParameter));
                        }
                        UserData.current.weapons.Add(newWeapon);
                        UserData.current.weaponCreatedNumber++;
                        SaveData.SaveUserData(UserData.current);
                        uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedWeapon, newWeapon);
                        uiView.ConfirmListRoot.SetActive(true);
                        
                        // 装備ボタンが押されたら装備する
                        uiView.EquipmentButton.Button.onClick.RemoveAllListeners();
                        uiView.EquipmentButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.equippedWeaponInstanceId = newWeapon.instanceId;
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedWeapon, newWeapon);
                            uiView.ConfirmListRoot.SetActive(false);
                        });
                        
                        // 破棄ボタンが押されたら削除する
                        uiView.DiscardButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.weapons.Remove(newWeapon);
                            uiView.ConfirmListRoot.SetActive(false);
                            uiView.EquipmentInformationUIView.SetDeactiveAll();
                        });

                        // 破棄して実行ボタンが押されたら削除してもう一度ガチャする
                        uiView.DiscardAndInvokeButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardAndInvokeButton.Button.onClick.AddListener(() =>
                        {
                            uiView.DiscardButton.Button.onClick.Invoke();
                            uiView.InvokeButton.Button.onClick.Invoke();
                        });
                    });
                });
            }
            foreach (var gachaId in UserData.current.unlockArmorGachas)
            {
                var gacha = MasterDataArmorGacha.Instance.gachas.Find(x => x.id == gachaId);
                var gachaButton = uiView.CreateGachaButton();
                gachaButton.NewIcon.SetActive(!userData.armorGachaInvokeCounts.ContainsKey(gachaId));
                gachaButton.Message.text = gacha.Name;
                gachaButton.Button.onClick.AddListener(() =>
                {
                    this.SetSelectedGachaButton(gachaButton);
                    uiView.InvokeListRoot.SetActive(true);
                    uiView.ConfirmListRoot.SetActive(false);
                    uiView.EquipmentInformationUIView.SetDeactiveAll();
                    uiView.InvokeButton.Message.text = string.Format(
                        LocalizeString.Get("UI", "InvokeMoneyFormat"),
                        gacha.money
                        );

                    // 実行ボタンが押されたらガチャする
                    uiView.InvokeButton.Button.onClick.RemoveAllListeners();
                    uiView.InvokeButton.Button.onClick.AddListener(() =>
                    {
                        if (!UserData.current.IsPossessionMoney(gacha.money))
                        {
                            UIManager.NotifyUIController.Show(LocalizeString.Get("UI", "NotEnoughMoney")).Forget();
                            return;
                        }
                        userData.AddMoney(-gacha.money);
                        if (!userData.armorGachaInvokeCounts.ContainsKey(gachaId))
                        {
                            userData.armorGachaInvokeCounts.Add(gachaId, 0);
                        }
                        userData.armorGachaInvokeCounts[gachaId]++;
                        gachaButton.NewIcon.SetActive(false);

                        var hitPoint = gacha.hitPoints.Lottery();
                        var physicalDefense = gacha.physicalDefenses.Lottery();
                        var magicDefense = gacha.magicDefenses.Lottery();
                        var speed = gacha.speeds.Lottery();
                        var newArmor = new Armor
                        {
                            instanceId = UserData.current.armorCreatedNumber,
                            nameKey = "Test",
                            hitPoint = new InstanceParameter
                            {
                                parameter = hitPoint.value.GetParameter(),
                                rare = hitPoint.value.rare
                            },
                            physicalDefense = new InstanceParameter
                            {
                                parameter = physicalDefense.value.GetParameter(),
                                rare = physicalDefense.value.rare
                            },
                            magicDefense = new InstanceParameter
                            {
                                parameter = magicDefense.value.GetParameter(),
                                rare = magicDefense.value.rare
                            },
                            speed = new InstanceParameter
                            {
                                parameter = speed.value.GetParameter(),
                                rare = speed.value.rare
                            }
                        };
                        UserData.current.armors.Add(newArmor);
                        UserData.current.armorCreatedNumber++;
                        SaveData.SaveUserData(UserData.current);
                        uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedArmor, newArmor);
                        uiView.ConfirmListRoot.SetActive(true);
                                                
                        // 装備ボタンが押されたら装備する
                        uiView.EquipmentButton.Button.onClick.RemoveAllListeners();
                        uiView.EquipmentButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.equippedArmorInstanceId = newArmor.instanceId;
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedArmor, newArmor);
                            uiView.ConfirmListRoot.SetActive(false);
                        });
                        
                        // 破棄ボタンが押されたら削除する
                        uiView.DiscardButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.armors.Remove(newArmor);
                            uiView.ConfirmListRoot.SetActive(false);
                            uiView.EquipmentInformationUIView.SetDeactiveAll();
                        });
                        
                        // 破棄して実行ボタンが押されたら削除してもう一度ガチャする
                        uiView.DiscardAndInvokeButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardAndInvokeButton.Button.onClick.AddListener(() =>
                        {
                            uiView.DiscardButton.Button.onClick.Invoke();
                            uiView.InvokeButton.Button.onClick.Invoke();
                        });
                    });
                });
            }
            foreach (var gachaId in UserData.current.unlockAccessoryGachas)
            {
                var gacha = MasterDataAccessoryGacha.Instance.gachas.Find(x => x.id == gachaId);
                var gachaButton = uiView.CreateGachaButton();
                gachaButton.NewIcon.SetActive(!userData.accessoryGachaInvokeCounts.ContainsKey(gachaId));
                gachaButton.Message.text = gacha.Name;
                gachaButton.Button.onClick.AddListener(() =>
                {
                    this.SetSelectedGachaButton(gachaButton);
                    uiView.InvokeListRoot.SetActive(true);
                    uiView.ConfirmListRoot.SetActive(false);
                    uiView.EquipmentInformationUIView.SetDeactiveAll();
                    uiView.InvokeButton.Message.text = string.Format(
                        LocalizeString.Get("UI", "InvokeMoneyFormat"),
                        gacha.money
                        );
                    
                    // 実行ボタンが押されたらガチャする
                    uiView.InvokeButton.Button.onClick.RemoveAllListeners();
                    uiView.InvokeButton.Button.onClick.AddListener(() =>
                    {
                        if (!UserData.current.IsPossessionMoney(gacha.money))
                        {
                            UIManager.NotifyUIController.Show(LocalizeString.Get("UI", "NotEnoughMoney")).Forget();
                            return;
                        }
                        userData.AddMoney(-gacha.money);
                        if (!userData.accessoryGachaInvokeCounts.ContainsKey(gachaId))
                        {
                            userData.accessoryGachaInvokeCounts.Add(gachaId, 0);
                        }
                        userData.accessoryGachaInvokeCounts[gachaId]++;
                        gachaButton.NewIcon.SetActive(false);
                        
                        var newAccessory = new Accessory
                        {
                            instanceId = UserData.current.accessoryCreatedNumber,
                            nameKey = "Test"
                        };
                        var skillNumber = gacha.skillNumbers.Lottery().value.GetParameter();
                        for (var i = 0; i < skillNumber; i++)
                        {
                            var instanceParameter = gacha.passiveSkillIds.Lottery().value;
                            var passiveSkill = MasterDataPassiveSkill.Instance.skills.Find(x => x.id == instanceParameter.parameter);
                            var attachNumber = newAccessory.passiveSkillIds
                                .Count(x => x.parameter == instanceParameter.parameter);
                            
                            // アタッチ可能数を超えていた場合は付与できない
                            if (passiveSkill.attachMax <= attachNumber)
                            {
                                // もう一度スキルを抽選する
                                i--;
                                continue;
                            }
                            newAccessory.passiveSkillIds.Add(new InstanceParameter(instanceParameter));
                        }
                        UserData.current.accessories.Add(newAccessory);
                        UserData.current.accessoryCreatedNumber++;
                        SaveData.SaveUserData(UserData.current);
                        uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedAccessory, newAccessory);
                        uiView.ConfirmListRoot.SetActive(true);
                                                                        
                        // 装備ボタンが押されたら装備する
                        uiView.EquipmentButton.Button.onClick.RemoveAllListeners();
                        uiView.EquipmentButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.equippedAccessoryInstanceId = newAccessory.instanceId;
                            uiView.EquipmentInformationUIView.Setup(UserData.current.EquippedAccessory, newAccessory);
                            uiView.ConfirmListRoot.SetActive(false);
                        });
                        
                        // 破棄ボタンが押されたら削除する
                        uiView.DiscardButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardButton.Button.onClick.AddListener(() =>
                        {
                            UserData.current.accessories.Remove(newAccessory);
                            uiView.ConfirmListRoot.SetActive(false);
                            uiView.EquipmentInformationUIView.SetDeactiveAll();
                        });
                        
                        // 破棄して実行ボタンが押されたら削除してもう一度ガチャする
                        uiView.DiscardAndInvokeButton.Button.onClick.RemoveAllListeners();
                        uiView.DiscardAndInvokeButton.Button.onClick.AddListener(() =>
                        {
                            uiView.DiscardButton.Button.onClick.Invoke();
                            uiView.InvokeButton.Button.onClick.Invoke();
                        });
                    });
                });
            }

            uiView.InvokeListRoot.SetActive(false);
            uiView.ConfirmListRoot.SetActive(false);
            uiView.EquipmentInformationUIView.SetDeactiveAll();
            HeaderUIViewUtility.Setup(uiView.HeaderUIView, scope);

            MessageBroker.Instance.GetSubscriber<SceneEvent.OnDestroy>()
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
