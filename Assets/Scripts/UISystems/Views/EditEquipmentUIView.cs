using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditEquipmentUIView : UIView
    {
        [SerializeField]
        private CookieButton weaponButton;

        [SerializeField]
        private CookieButton armorButton;

        [SerializeField]
        private CookieButton accessoryButton;

        [SerializeField]
        private RectTransform equipmentRoot;

        [SerializeField]
        private CookieButton equipmentButtonPrefab;

        [SerializeField]
        private GameObject confirmRoot;

        [SerializeField]
        private CookieButton equipmentButton;

        [SerializeField]
        private CookieButton discardButton;

        [SerializeField]
        private HeaderUIView headerUIView;

        [SerializeField]
        private EquipmentInformationUIView equipmentInformationUIView;
        
        private readonly List<CookieButton> equipmentButtons = new();

        public CookieButton WeaponButton => this.weaponButton;

        public CookieButton ArmorButton => this.armorButton;

        public CookieButton AccessoryButton => this.accessoryButton;

        public GameObject ConfirmRoot => this.confirmRoot;

        public CookieButton EquipmentButton => this.equipmentButton;

        public CookieButton DiscardButton => this.discardButton;

        public HeaderUIView HeaderUIView => this.headerUIView;

        public EquipmentInformationUIView EquipmentInformationUIView => this.equipmentInformationUIView;
        
        public void DestroyAllEquipmentButtons()
        {
            foreach (var equipmentButton in this.equipmentButtons)
            {
                Destroy(equipmentButton.gameObject);
            }
            
            this.equipmentButtons.Clear();
        }

        public CookieButton CreateEquipmentButton()
        {
            var result = Instantiate(this.equipmentButtonPrefab, this.equipmentRoot);
            this.equipmentButtons.Add(result);

            return result;
        }
    }
}
