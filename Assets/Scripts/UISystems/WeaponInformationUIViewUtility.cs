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
        public static void Setup(WeaponInformationUIView uiView, Weapon weapon)
        {
            uiView.WeaponName.text = UserData.current.equippedWeaponInstanceId == weapon.instanceId
                ? $"[E] {weapon.Name}"
                : weapon.Name;
            uiView.PhysicalStrength.SetText("{0}", weapon.physicalStrength.parameter);
            uiView.MagicStrength.SetText("{0}", weapon.magicStrength.parameter);
            uiView.TotalStrength.SetText("{0}", weapon.TotalStrength);
            uiView.CriticalRate.SetText("{0}%", weapon.criticalRate.parameter);
            uiView.PhysicalStrengthComparisonUIStylists.Apply(0);
            uiView.MagicStrengthComparisonUIStylists.Apply(0);
            uiView.TotalStrengthComparisonUIStylists.Apply(0);
            uiView.CriticalRateComparisonUIStylists.Apply(0);
            uiView.DestroyAllActiveSkillUIElements();
            for (var i = 0; i < weapon.activeSkillIds.Count; i++)
            {
                var activeSkillId = weapon.activeSkillIds[i];
                var activeSkill = MasterDataActiveSkill.Instance.skills.Find(activeSkillId.parameter);
                var activeSkillUIElement = uiView.CreateActiveSkillUIElement();
                activeSkillUIElement.Index.SetText("{0}", i + 1);
                activeSkillUIElement.NameText.text = activeSkill.Name;
                activeSkillUIElement.CreateRareEffect(activeSkillId.rare);
            }
            
            uiView.DestroyAllRareEffects();
            CreateRareEffect(uiView, weapon.physicalStrength.rare, uiView.PhysicalStrengthEffectParent);
            CreateRareEffect(uiView, weapon.magicStrength.rare, uiView.MagicStrengthEffectParent);
            CreateRareEffect(uiView, weapon.criticalRate.rare, uiView.CriticalRateEffectParent);
        }
        
        public static void Setup(WeaponInformationUIView uiView, Weapon before, Weapon after)
        {
            Setup(uiView, after);
            uiView.PhysicalStrengthComparisonUIStylists.Apply(after.physicalStrength.parameter - before.physicalStrength.parameter);
            uiView.MagicStrengthComparisonUIStylists.Apply(after.magicStrength.parameter - before.magicStrength.parameter);
            uiView.TotalStrengthComparisonUIStylists.Apply(after.TotalStrength - before.TotalStrength);
            uiView.CriticalRateComparisonUIStylists.Apply(after.criticalRate.parameter - before.criticalRate.parameter);
        }

        private static void CreateRareEffect(WeaponInformationUIView uiView, Rare rare, Transform parent)
        {
            var effect = uiView.CreateRareEffect(rare);
            effect.transform.SetParent(parent, false);
        }
    }
}
