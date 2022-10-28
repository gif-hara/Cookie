using Cookie.UISystems;
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
    }
}
