using Cookie.UISystems;
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
        public static void Setup(HeaderUIView headerUIView)
        {
            headerUIView.RootButton.Button.onClick.AddListener(() =>
            {
                UIManager.SetAsLastSibling(UIManager.StartMenuUIView);
                UIManager.Show(UIManager.StartMenuUIView);
            });
        }
    }
}
