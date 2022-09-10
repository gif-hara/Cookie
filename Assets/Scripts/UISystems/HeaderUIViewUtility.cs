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
                headerUIView.MenuRoot.SetActive(!headerUIView.MenuRoot.activeSelf);
            });
            
            headerUIView.GachaButton.Button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Gacha");
            });
            
            headerUIView.EditEquipmentButton.Button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("EditEquipment");
            });
        }
    }
}
