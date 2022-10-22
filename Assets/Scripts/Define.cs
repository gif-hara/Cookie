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
    
    public static class SkillAttributeName
    {
        public const string Power = "Power";
        
        public const string AttackAttribute = "AttackAttribute";

        public const string ActiveSkillType = "ActiveSkillType";

        public const string PhysicalStrengthUpRate = "PhysicalStrengthUpRate";

        public const string MagicStrengthUpRate = "MagicStrengthUpRate";

        public const string PhysicalDefenseUpRate = "PhysicalDefenseUpRate";

        public const string MagicDefenseUpRate = "MagicDefenseUpRate";
        
        public const string RecoveryUpRate = "RecoveryUpRate";
    }
}
