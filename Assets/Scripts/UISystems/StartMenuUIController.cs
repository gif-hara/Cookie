using System;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
            
            this.uiView.GachaButton.Button.onClick.AddListener(async () =>
            {
                await SceneManager.LoadSceneAsync("Gacha");
                UIManager.Hidden(this.uiView);
            });
            
            this.uiView.EditEquipmentButton.Button.onClick.AddListener(async () =>
            {
                await SceneManager.LoadSceneAsync("EditEquipment");
                UIManager.Hidden(this.uiView);
            });
            
            this.uiView.SelectEnemyButton.Button.onClick.AddListener(async () =>
            {
                await SceneManager.LoadSceneAsync("SelectEnemy");
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
