using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;

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
            foreach (var enemyStatus in MasterDataEnemyStatus.Instance.enemyStatusList)
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
                        Debug.Log(enemyStatus);
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
