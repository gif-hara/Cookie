using Cookie.UISystems;
using Cysharp.Threading.Tasks;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FadeUIController
    {
        private FadeUIView uiView;
        
        public void Setup(FadeUIView prefab)
        {
            this.uiView = UIManager.Open(prefab);
            UIManager.Hidden(this.uiView);
        }

        public UniTask PlayInAsync(FadeUIView.FadeType fadeType, UniTask awaiter)
        {
            return this.uiView.PlayInAsync(fadeType);
        }

        public UniTask PlayOutAsync(FadeUIView.FadeType fadeType, UniTask awaiter)
        {
            return this.uiView.PlayOutAsync(fadeType);
        }
    }
}
