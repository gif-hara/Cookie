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

        [SerializeField]
        private GameObject invokeListRoot;

        [SerializeField]
        private CookieButton invokeButton;

        [SerializeField]
        private EquipmentInformationUIView equipmentInformationUIView;

        [SerializeField]
        private GameObject confirmListRoot;
        
        [SerializeField]
        private CookieButton equipmentButton;

        [SerializeField]
        private CookieButton discardButton;

        [SerializeField]
        private CookieButton discardAndInvokeButton;

        private readonly List<CookieButton> gachaButtons = new();

        public HeaderUIView HeaderUIView => this.headerUIView;

        public CookieButton WeaponGachaButton => this.weaponGachaButton;
        
        public CookieButton ArmorGachaButton => this.armorGachaButton;

        public CookieButton AccessoryGachaButton => this.accessoryGachaButton;

        public GameObject InvokeListRoot => this.invokeListRoot;

        public CookieButton InvokeButton => this.invokeButton;

        public EquipmentInformationUIView EquipmentInformationUIView => this.equipmentInformationUIView;

        public GameObject ConfirmListRoot => this.confirmListRoot;

        public CookieButton EquipmentButton => this.equipmentButton;

        public CookieButton DiscardButton => this.discardButton;

        public CookieButton DiscardAndInvokeButton => this.discardAndInvokeButton;
        
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
