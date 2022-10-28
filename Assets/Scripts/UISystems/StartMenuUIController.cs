using Cookie.UISystems;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StartMenuUIController
    {
        private StartMenuUIView uiView;
        
        public void Setup(StartMenuUIView prefab)
        {
            this.uiView = UIManager.Open(prefab);
            UIManager.Hidden(this.uiView);
            
            this.uiView.GachaButton.Button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Gacha");
                UIManager.Hidden(this.uiView);
            });
            
            this.uiView.EditEquipmentButton.Button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("EditEquipment");
                UIManager.Hidden(this.uiView);
            });
            
            this.uiView.SelectEnemyButton.Button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("SelectEnemy");
                UIManager.Hidden(this.uiView);
            });
        }

        public void Show()
        {
            UIManager.SetAsLastSibling(this.uiView);
            UIManager.Show(this.uiView);
        }
    }
}
