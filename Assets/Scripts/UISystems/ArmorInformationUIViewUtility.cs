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
        public static void Setup(ArmorInformationUIView armorInformationUIView, Armor armor)
        {
            armorInformationUIView.ArmorName.text = UserData.current.equippedArmorInstanceId == armor.instanceId
                ? $"[E] {armor.Name}"
                : armor.Name;
            armorInformationUIView.PhysicalDefense.text = armor.physicalDefense.ToString();
            armorInformationUIView.MagicDefense.text = armor.magicDefense.ToString();
            armorInformationUIView.HitPoint.text = armor.hitPoint.ToString();
            armorInformationUIView.Speed.text = armor.speed.ToString();
            armorInformationUIView.PhysicalDefenseComparisonUIStylists.Apply(0);
            armorInformationUIView.MagicDefenseComparisonUIStylists.Apply(0);
            armorInformationUIView.HitPointComparisonUIStylists.Apply(0);
            armorInformationUIView.SpeedComparisonUIStylists.Apply(0);
        }
        
        public static void Setup(ArmorInformationUIView armorInformationUIView, Armor before, Armor after)
        {
            Setup(armorInformationUIView, after);
            armorInformationUIView.PhysicalDefenseComparisonUIStylists.Apply(after.physicalDefense - before.physicalDefense);
            armorInformationUIView.MagicDefenseComparisonUIStylists.Apply(after.magicDefense - before.magicDefense);
            armorInformationUIView.HitPointComparisonUIStylists.Apply(after.hitPoint - before.hitPoint);
            armorInformationUIView.SpeedComparisonUIStylists.Apply(after.speed - before.speed);
        }
    }
}
