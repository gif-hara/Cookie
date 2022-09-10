using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class WeaponInformationUIViewUtility
    {
        public static void Setup(WeaponInformationUIView weaponInformationUIView, Weapon weapon)
        {
            weaponInformationUIView.WeaponName.text = UserData.current.equippedWeaponInstanceId == weapon.instanceId
                ? $"[E] {weapon.Name}"
                : weapon.Name;
            weaponInformationUIView.PhysicalStrength.text = weapon.physicalStrength.ToString();
            weaponInformationUIView.MagicStrength.text = weapon.magicStrength.ToString();
            weaponInformationUIView.TotalStrength.text = weapon.TotalStrength.ToString();
            weaponInformationUIView.PhysicalStrengthComparisonUIStylists.Apply(0);
            weaponInformationUIView.MagicStrengthComparisonUIStylists.Apply(0);
            weaponInformationUIView.TotalStrengthComparisonUIStylists.Apply(0);
            weaponInformationUIView.DestroyAllActiveSkillUIElements();
            for (var i = 0; i < weapon.activeSkillIds.Count; i++)
            {
                var activeSkillId = weapon.activeSkillIds[i];
                var activeSkill = MasterDataActiveSkill.Instance.skills.Find(activeSkillId);
                var activeSkillUIElement = weaponInformationUIView.CreateActiveSkillUIElement();
                activeSkillUIElement.Index.text = (i + 1).ToString();
                activeSkillUIElement.NameText.text = activeSkill.Name;
            }
        }
        public static void Setup(WeaponInformationUIView weaponInformationUIView, Weapon before, Weapon after)
        {
            Setup(weaponInformationUIView, after);
            weaponInformationUIView.PhysicalStrengthComparisonUIStylists.Apply(after.physicalStrength - before.physicalStrength);
            weaponInformationUIView.MagicStrengthComparisonUIStylists.Apply(after.magicStrength - before.magicStrength);
            weaponInformationUIView.TotalStrengthComparisonUIStylists.Apply(after.TotalStrength - before.TotalStrength);
        }
    }
}