using System.Collections.Generic;
using UnityEngine;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SelectEnemyUIView : UIView
    {
        [SerializeField]
        private HeaderUIView headerUIView;
        
        [SerializeField]
        private Transform enemyListRoot;

        [SerializeField]
        private CookieButton buttonPrefab;
        
        [SerializeField]
        private GameObject confirmListRoot;
        
        [SerializeField]
        private CookieButton battleButton;
        
        private readonly List<CookieButton> enemyButtons = new();

        public HeaderUIView HeaderUIView => this.headerUIView;
        
        public GameObject ConfirmListRoot => this.confirmListRoot;

        public CookieButton BattleButton => this.battleButton;
        
        public void DestroyAllEnemyButtons()
        {
            foreach (var gachaButton in this.enemyButtons)
            {
                Destroy(gachaButton.gameObject);
            }
            
            this.enemyButtons.Clear();
        }

        public CookieButton CreateEnemyButton()
        {
            var result = Instantiate(this.buttonPrefab, this.enemyListRoot);
            this.enemyButtons.Add(result);

            return result;
        }
    }
}
