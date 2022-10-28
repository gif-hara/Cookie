using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NotifyUIController
    {
        private NotifyUIView uiView;
        
        public void Setup(NotifyUIView prefab)
        {
            this.uiView = UIManager.Open(prefab);
            UIManager.Hidden(this.uiView);
        }

        public async UniTask Show(params string[] messages)
        {
            this.uiView.SetAsLastSibling();
            this.uiView.Show();
            
            foreach (var message in messages)
            {
                this.uiView.Message.text = message;
                await this.uiView.Button.OnClickAsync();
            }
            
            this.uiView.Hidden();
        }
    }
}
