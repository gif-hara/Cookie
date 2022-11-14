using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class ArmorInformationUIViewUtility
    {
        public static void Setup(ArmorInformationUIView uiView, Armor armor)
        {
            uiView.ArmorName.text = UserData.current.equippedArmorInstanceId == armor.instanceId
                ? $"[E] {armor.Name}"
                : armor.Name;
            uiView.PhysicalDefense.SetText("{0}", armor.physicalDefense.parameter);
            uiView.MagicDefense.SetText("{0}", armor.magicDefense.parameter);
            uiView.HitPoint.SetText("{0}", armor.hitPoint.parameter);
            uiView.Speed.SetText("{0}", armor.speed.parameter);
            uiView.PhysicalDefenseComparisonUIStylists.Apply(0);
            uiView.MagicDefenseComparisonUIStylists.Apply(0);
            uiView.HitPointComparisonUIStylists.Apply(0);
            uiView.SpeedComparisonUIStylists.Apply(0);
            
            uiView.DestroyAllRareEffects();
            CreateRareEffect(uiView, armor.hitPoint.rare, uiView.HitPointEffectParent);
            CreateRareEffect(uiView, armor.physicalDefense.rare, uiView.PhysicalDefenseEffectParent);
            CreateRareEffect(uiView, armor.magicDefense.rare, uiView.MagicDefenseEffectParent);
            CreateRareEffect(uiView, armor.speed.rare, uiView.SpeedEffectParent);
        }
        
        public static void Setup(ArmorInformationUIView uiView, Armor before, Armor after)
        {
            Setup(uiView, after);
            uiView.PhysicalDefenseComparisonUIStylists.Apply(after.physicalDefense.parameter - before.physicalDefense.parameter);
            uiView.MagicDefenseComparisonUIStylists.Apply(after.magicDefense.parameter - before.magicDefense.parameter);
            uiView.HitPointComparisonUIStylists.Apply(after.hitPoint.parameter - before.hitPoint.parameter);
            uiView.SpeedComparisonUIStylists.Apply(after.speed.parameter - before.speed.parameter);
        }
        
        private static void CreateRareEffect(ArmorInformationUIView uiView, Rare rare, Transform parent)
        {
            var effect = uiView.CreateRareEffect(rare);
            effect.transform.SetParent(parent, false);
        }
    }
}
