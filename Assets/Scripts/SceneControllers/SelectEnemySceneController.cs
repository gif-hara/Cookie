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
        
        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var uiView = UIManager.Open(this.selectEnemyUIPrefab);
            uiView.DestroyAllFieldButtons();
            uiView.DestroyAllEnemyButtons();
            var enemyGroupByFieldId = UserData.current.unlockEnemies
                .Select(x => MasterDataEnemyStatus.Instance.enemyStatusList.Find(y => x == y.id))
                .GroupBy(x => x.fieldId);
            foreach (var enemyGroup in enemyGroupByFieldId)
            {
                var fieldButton = uiView.CreateFieldButton();
                fieldButton.Message.text = MasterDataFieldData.Instance.records.Find(x => x.id == enemyGroup.Key).Name;
                fieldButton.Button.onClick.AddListener(() =>
                {
                    SetSelectedFieldButton(fieldButton);
                    uiView.DestroyAllEnemyButtons();
                    uiView.ConfirmListRoot.SetActive(false);

                    foreach (var enemyStatus in enemyGroup)
                    {
                        var enemyButton = uiView.CreateEnemyButton();
                        enemyButton.Message.text = enemyStatus.Name;
                        enemyButton.Button.onClick.AddListener(() =>
                        {
                            this.SetSelectedEnemyButton(enemyButton);
                            uiView.ConfirmListRoot.SetActive(true);
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
                                        
                                        // お金加算
                                        userData.money += enemyStatus.money;

                                        // 倒した敵の数を加算する
                                        if (!userData.defeatedEnemies.ContainsKey(enemyStatus.id))
                                        {
                                            userData.defeatedEnemies.Add(enemyStatus.id, 0);
                                        }
                                        userData.defeatedEnemies[enemyStatus.id]++;

                                        // 各種コンテンツのアンロック処理
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
                                                    Assert.IsTrue(false, $"{i.unlockType}は未対応です");
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

            this.MessageBroker.GetSubscriber<SceneEvent.OnDestroy>()
                .Subscribe(_ =>
                {
                    UIManager.Close(uiView);
                })
                .AddTo(scope);
            
            return base.OnStartAsync(scope);
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
