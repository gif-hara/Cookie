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
        private GameObject weaponInformationRoot;

        [SerializeField]
        private WeaponInformationUIView beforeWeaponInformationUIView;

        [SerializeField]
        private WeaponInformationUIView afterWeaponInformationUIView;

        [SerializeField]
        private GameObject armorInformationRoot;

        [SerializeField]
        private ArmorInformationUIView beforeArmorInformationUIView;

        [SerializeField]
        private ArmorInformationUIView afterArmorInformationUIView;

        private readonly List<CookieButton> equipmentButtons = new();

        public CookieButton WeaponButton => this.weaponButton;

        public CookieButton ArmorButton => this.armorButton;

        public CookieButton AccessoryButton => this.accessoryButton;

        public GameObject ConfirmRoot => this.confirmRoot;

        public CookieButton EquipmentButton => this.equipmentButton;

        public CookieButton DiscardButton => this.discardButton;

        public HeaderUIView HeaderUIView => this.headerUIView;

        public WeaponInformationUIView BeforeWeaponInformationUIView => this.beforeWeaponInformationUIView;

        public WeaponInformationUIView AfterWeaponInformationUIView => this.afterWeaponInformationUIView;

        public ArmorInformationUIView BeforeArmorInformationUIView => this.beforeArmorInformationUIView;

        public ArmorInformationUIView AfterArmorInformationUIView => this.afterArmorInformationUIView;

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

        public void SetActiveEquipmentInformationRoot(EquipmentType type)
        {
            this.weaponInformationRoot.SetActive(type == EquipmentType.Weapon);
            this.armorInformationRoot.SetActive(type == EquipmentType.Armor);
        }

        public void SetDeactiveEquipmentInformationRoot()
        {
            this.weaponInformationRoot.SetActive(false);
            this.armorInformationRoot.SetActive(false);
        }
    }
}
