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
    public static class HeaderUIViewUtility
    {
        public static void Setup(HeaderUIView headerUIView, DisposableBagBuilder scope)
        {
            headerUIView.RootButton.Button.onClick.AddListener(() =>
            {
                UIManager.StartMenuUIController.Show();
            });
            
            UpdateMoney(headerUIView, UserData.current.Money);
            GlobalMessagePipe.GetSubscriber<UserDataEvent.UpdatedMoney>()
                .Subscribe(_ =>
                {
                    UpdateMoney(headerUIView, UserData.current.Money);
                })
                .AddTo(scope);
        }

        private static void UpdateMoney(HeaderUIView headerUIView, int money)
        {
            headerUIView.Money.text = money.ToString();
        }
    }
}
