using System.Collections.Generic;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using System.Linq;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PopupUIController
    {
        private PopupUIView uiView;
        
        public void Setup(PopupUIView prefab)
        {
            this.uiView = UIManager.Open(prefab);
            UIManager.Hidden(this.uiView);
        }
        
        public UniTask<bool> ShowAsync(string message)
        {
            return this.ShowAsync(
                message,
                LocalizeString.Get("UI", "Yes"),
                LocalizeString.Get("UI", "No")
                );
        }

        public async UniTask<bool> ShowAsync(string message, string yes, string no)
        {
            this.uiView.Message.text = message;
            this.uiView.YesButton.Message.text = yes;
            this.uiView.NoButton.Message.text = no;
            
            UIManager.Show(this.uiView);
            UIManager.SetAsLastSibling(this.uiView);
            
            var index = await UniTask.WhenAny(
                this.uiView.YesButton.Button.OnClickAsync(),
                this.uiView.NoButton.Button.OnClickAsync()
                );
            
            UIManager.Hidden(this.uiView);
            
            return index == 0;
        }
    }
}
