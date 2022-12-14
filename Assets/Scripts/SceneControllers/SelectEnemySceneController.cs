using System;
using System.Linq;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SelectEnemySceneController : SceneController
    {
        [SerializeField]
        private SelectEnemyUIView selectEnemyUIPrefab;

        private CookieButton selectedFieldButton;

        private CookieButton selectedEnemyButton;
        
        protected override async UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var uiView = UIManager.Open(this.selectEnemyUIPrefab);
            HeaderUIViewUtility.Setup(uiView.HeaderUIView, scope);
            uiView.DestroyAllFieldButtons();
            uiView.DestroyAllEnemyButtons();
            uiView.SetActiveEnemyInformation(false);
            var enemyGroupByFieldId = UserData.current.unlockEnemies
                .Select(x => MasterDataEnemyStatus.Instance.enemyStatusList.Find(y => x == y.id))
                .GroupBy(x => x.fieldId);
            foreach (var enemyGroup in enemyGroupByFieldId)
            {
                var isInNewEnemy = enemyGroup.Any(enemyStatus => !UserData.current.defeatedEnemies.ContainsKey(enemyStatus.id));
                var fieldButton = uiView.CreateFieldButton();
                fieldButton.NewIcon.SetActive(isInNewEnemy);
                fieldButton.Message.text = MasterDataFieldData.Instance.records.Find(x => x.id == enemyGroup.Key).Name;
                fieldButton.Button.onClick.AddListener(() =>
                {
                    SetSelectedFieldButton(fieldButton);
                    uiView.DestroyAllEnemyButtons();
                    uiView.ConfirmListRoot.SetActive(false);

                    foreach (var enemyStatus in enemyGroup)
                    {
                        var isNew = !UserData.current.defeatedEnemies.ContainsKey(enemyStatus.id);
                        isInNewEnemy |= isNew;
                        var enemyButton = uiView.CreateEnemyButton();
                        enemyButton.NewIcon.SetActive(isNew);
                        enemyButton.Message.text = enemyStatus.Name;
                        enemyButton.Button.onClick.AddListener(() =>
                        {
                            this.SetSelectedEnemyButton(enemyButton);
                            uiView.ConfirmListRoot.SetActive(true);
                            uiView.SetupEnemyInformation(enemyStatus);
                            uiView.SetActiveEnemyInformation(!isNew);
                            uiView.BattleButton.Button.onClick.RemoveAllListeners();
                            uiView.BattleButton.Button.onClick.AddListener(() =>
                            {
                                var battleSceneArgument = new BattleSceneArgument
                                {
                                    playerStatusBuilder = UserData.current.ToActorStatusBuilder(),
                                    enemyStatusBuilder = enemyStatus.ToActorStatusBuilder()
                                };
                                battleSceneArgument.onBattleEnd = judgement =>
                                {
                                    if (judgement == BattleJudgement.PlayerWin)
                                    {
                                        var userData = UserData.current;
                                        
                                        // ????????????
                                        userData.AddMoney(enemyStatus.money);

                                        // ?????????????????????????????????
                                        if (!userData.defeatedEnemies.ContainsKey(enemyStatus.id))
                                        {
                                            userData.defeatedEnemies.Add(enemyStatus.id, 0);
                                        }
                                        userData.defeatedEnemies[enemyStatus.id]++;

                                        // ?????????????????????????????????????????????
                                        foreach (var i in enemyStatus.defeatEnemyUnlocks)
                                        {
                                            switch (i.unlockType)
                                            {
                                                case UnlockType.Enemy:
                                                    userData.UnlockEnemy(i.unlockId);
                                                    break;
                                                case UnlockType.WeaponGacha:
                                                    userData.UnlockWeaponGacha(i.unlockId);
                                                    break;
                                                case UnlockType.ArmorGacha:
                                                    userData.UnlockArmorGacha(i.unlockId);
                                                    break;
                                                case UnlockType.AccessoryGacha:
                                                    userData.UnlockAccessoryGacha(i.unlockId);
                                                    break;
                                                default:
                                                    Assert.IsTrue(false, $"{i.unlockType}??????????????????");
                                                    break;
                                            }
                                        }
                                    }
                                };
                                battleSceneArgument.onBattleFinalize = () =>
                                {
                                    SceneMediator.SetArgument(null);
                                    SceneManager.LoadScene("SelectEnemy");
                                };
                                SceneMediator.SetArgument(battleSceneArgument);
                                SceneManager.LoadScene("Battle");
                            });
                        });
                    }
                });
            }
            
            uiView.ConfirmListRoot.SetActive(false);

            MessageBroker.Instance.GetSubscriber<SceneEvent.OnDestroy>()
                .Subscribe(_ =>
                {
                    UIManager.Close(uiView);
                })
                .AddTo(scope);

            await UIManager.NotifyUIController.ShowUnlockContents();
        }

        private void SetSelectedFieldButton(CookieButton fieldButton)
        {
            if (this.selectedFieldButton != null)
            {
                this.selectedFieldButton.PositiveStylist.Apply();
            }

            this.selectedFieldButton = fieldButton;

            if (this.selectedFieldButton != null)
            {
                this.selectedFieldButton.SelectedStylist.Apply();
            }
        }

        private void SetSelectedEnemyButton(CookieButton enemyButton)
        {
            if (this.selectedEnemyButton != null)
            {
                this.selectedEnemyButton.PositiveStylist.Apply();
            }

            this.selectedEnemyButton = enemyButton;

            if (this.selectedEnemyButton != null)
            {
                this.selectedEnemyButton.SelectedStylist.Apply();
            }
        }
    }
}
