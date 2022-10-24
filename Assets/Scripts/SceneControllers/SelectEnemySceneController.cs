using System;
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
        private Transform uiParent;

        [SerializeField]
        private SelectEnemyUIView selectEnemyUIPrefab;

        private CookieButton selectedEnemyButton;
        
        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var uiView = Instantiate(this.selectEnemyUIPrefab, this.uiParent);
            uiView.DestroyAllEnemyButtons();
            foreach (var enemyId in UserData.current.unlockEnemies)
            {
                var enemyStatus = MasterDataEnemyStatus.Instance.enemyStatusList.Find(x => x.id == enemyId);
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
                                
                                // 各種コンテンツのアンロック処理
                                foreach (var i in enemyStatus.defeatEnemyUnlocks)
                                {
                                    switch (i.unlockType)
                                    {
                                        case UnlockType.Enemy:
                                            if (!userData.unlockEnemies.Contains(i.unlockId))
                                            {
                                                userData.unlockEnemies.Add(i.unlockId);
                                            }
                                            break;
                                        case UnlockType.WeaponGacha:
                                        case UnlockType.ArmorGacha:
                                        case UnlockType.AccessoryGacha:
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
            
            uiView.ConfirmListRoot.SetActive(false);
            
            return base.OnStartAsync(scope);
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
