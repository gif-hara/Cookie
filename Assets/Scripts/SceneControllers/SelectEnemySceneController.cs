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
        
        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            var uiView = Instantiate(this.selectEnemyUIPrefab, this.uiParent);
            uiView.DestroyAllEnemyButtons();
            foreach (var enemyStatus in MasterDataEnemyStatus.Instance.enemyStatusList)
            {
                var enemyButton = uiView.CreateEnemyButton();
                enemyButton.Message.text = enemyStatus.Name;
            }
            return base.OnStartAsync(scope);
        }
    }
}
