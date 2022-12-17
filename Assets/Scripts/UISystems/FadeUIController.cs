using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using MessagePipe;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FadeUIController
    {
        private FadeUIView uiView;
        
        public void Setup(FadeUIView prefab, DisposableBagBuilder bag)
        {
            this.uiView = UIManager.Open(prefab);
            UIManager.Hidden(this.uiView);

            MessageBroker.Instance.GetAsyncSubscriber<GlobalEvent.WillChangeScene>()
                .Subscribe( async(_, x) =>
                {
                    UIManager.Show(this.uiView);
                    UIManager.SetAsLastSibling(this.uiView);
                    await this.PlayInAsync(FadeUIView.FadeType.Basic);
                })
                .AddTo(bag);

            MessageBroker.Instance.GetAsyncSubscriber<GlobalEvent.ChangedScene>()
                .Subscribe( async(_, x) =>
                {
                    UIManager.SetAsLastSibling(this.uiView);
                    await this.PlayOutAsync(FadeUIView.FadeType.Basic);
                    UIManager.Hidden(this.uiView);
                })
                .AddTo(bag);
        }

        public UniTask PlayInAsync(FadeUIView.FadeType fadeType)
        {
            return this.uiView.PlayInAsync(fadeType);
        }

        public UniTask PlayOutAsync(FadeUIView.FadeType fadeType)
        {
            return this.uiView.PlayOutAsync(fadeType);
        }
    }
}
