using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EquipmentInformationUIView : MonoBehaviour
    {
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

        [SerializeField]
        private GameObject accessoryInformationRoot;

        [SerializeField]
        private AccessoryInformationUIView beforeAccessoryInformationUIView;

        [SerializeField]
        private AccessoryInformationUIView afterAccessoryInformationUIView;

        public void Setup(Weapon before, Weapon after)
        {
            this.SetActiveRoot(EquipmentType.Weapon);
            WeaponInformationUIViewUtility.Setup(this.beforeWeaponInformationUIView, before);
            WeaponInformationUIViewUtility.Setup(this.afterWeaponInformationUIView, before, after);
        }

        public void Setup(Armor before, Armor after)
        {
            this.SetActiveRoot(EquipmentType.Armor);
            ArmorInformationUIViewUtility.Setup(this.beforeArmorInformationUIView, before);
            ArmorInformationUIViewUtility.Setup(this.afterArmorInformationUIView, before, after);
        }

        public void Setup(Accessory before, Accessory after)
        {
            this.SetActiveRoot(EquipmentType.Accessory);
            AccessoryInformationUIViewUtility.Setup(this.beforeAccessoryInformationUIView, before);
            AccessoryInformationUIViewUtility.Setup(this.afterAccessoryInformationUIView, after);
        }

        private void SetActiveRoot(EquipmentType type)
        {
            this.weaponInformationRoot.SetActive(type == EquipmentType.Weapon);
            this.armorInformationRoot.SetActive(type == EquipmentType.Armor);
            this.accessoryInformationRoot.SetActive(type == EquipmentType.Accessory);
        }

        public void SetDeactiveAll()
        {
            this.weaponInformationRoot.SetActive(false);
            this.armorInformationRoot.SetActive(false);
            this.accessoryInformationRoot.SetActive(false);
        }
    }
}
