using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GachaUIView : UIView
    {
        [SerializeField]
        private HeaderUIView headerUIView;

        [SerializeField]
        private CookieButton weaponGachaButton;

        [SerializeField]
        private CookieButton armorGachaButton;

        [SerializeField]
        private CookieButton accessoryGachaButton;

        [SerializeField]
        private Transform gachaListRoot;

        [SerializeField]
        private CookieButton gachaButtonPrefab;

        private readonly List<CookieButton> gachaButtons = new();

        public HeaderUIView HeaderUIView => this.headerUIView;

        public CookieButton WeaponGachaButton => this.weaponGachaButton;
        
        public CookieButton ArmorGachaButton => this.armorGachaButton;

        public CookieButton AccessoryGachaButton => this.accessoryGachaButton;

        public void DestroyAllGachaButtons()
        {
            foreach (var gachaButton in this.gachaButtons)
            {
                Destroy(gachaButton.gameObject);
            }
            
            this.gachaButtons.Clear();
        }

        public CookieButton CreateGachaButton()
        {
            var result = Instantiate(this.gachaButtonPrefab, this.gachaListRoot);
            this.gachaButtons.Add(result);

            return result;
        }
    }
}
