using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Accessory,
    }

    public enum ActiveSkillType
    {
        Attack = 1,
        Recovery = 2,
    }

    public enum PassiveSkillType
    {
        /// <summary>
        /// 物理攻撃力UP
        /// </summary>
        PhysicalAttackUp,
        
        /// <summary>
        /// 魔法攻撃力UP
        /// </summary>
        MagicAttackUp,
        
        /// <summary>
        /// 物理防御力UP
        /// </summary>
        PhysicalDefenseUp,
        
        /// <summary>
        /// 魔法防御力UP
        /// </summary>
        MagicDefenseUp,
        
        /// <summary>
        /// 回復力UP
        /// </summary>
        RecoveryUp,
        
        /// <summary>
        /// 反撃
        /// </summary>
        CounterAttack,
        
        /// <summary>
        /// 吸収
        /// </summary>
        Absorption,
    }

    public enum ActorType
    {
        Player,
        Enemy,
    }

    /// <summary>
    /// 攻撃属性
    /// </summary>
    public enum AttackAttribute
    {
        /// <summary>
        /// 物理
        /// </summary>
        Physical = 1,
        
        /// <summary>
        /// 魔法
        /// </summary>
        Magic = 2,
    }

    /// <summary>
    /// レアタイプ
    /// </summary>
    public enum Rare
    {
        Common,
        UnCommon,
        Epic,
        Legendary,
    }

    public static class EquipmentAttributeName
    {
        public const string Strength = "Strength";
    }

    public static class SkillAttributeName
    {
        public const string Power = "Power";
        
        public const string AttackAttribute = "AttackAttribute";

        public const string ActiveSkillType = "ActiveSkillType";

        public const string PhysicalStrengthUpRate = "PhysicalStrengthUpRate";

        public const string MagicStrengthUpRate = "MagicStrengthUpRate";
    }
}
