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

    public enum AbnormalStatus
    {
        /// <summary>
        /// 毒
        /// </summary>
        Poison = 1,
        
        /// <summary>
        /// 麻痺
        /// </summary>
        Paralysis,
        
        /// <summary>
        /// 衰弱
        /// </summary>
        Debility,
        
        /// <summary>
        /// 脆弱
        /// </summary>
        Fragility,
    }
    
    public static class SkillAttributeName
    {
        /// <summary>
        /// 回復行動を行える
        /// </summary>
        public const string BehaviourAttack = "Behaviour.Attack";
        
        /// <summary>
        /// 回復行動を行える
        /// </summary>
        public const string BehaviourRecovery = "Behaviour.Recovery";

        /// <summary>
        /// 状態異常の付与を行える
        /// </summary>
        public const string BehaviourAddAbnormalStatus = "Behaviour.AddAbnormalStatus";

        /// <summary>
        /// 状態異常の解除を行える
        /// </summary>
        public const string BehaviourRemoveAbnormalStatus = "Behaviour.RemoveAbnormalStatus";
        
        public const string AttackPower = "Attack.Power";
        
        public const string AttackAttribute = "Attack.Attribute";

        public const string RecoveryPower = "Recovery.Power";
        
        public const string AddAbnormalStatusType = "AddAbnormalStatus.Type";
        
        public const string RemoveAbnormalStatusType = "RemoveAbnormalStatus.Type";

        public const string PhysicalStrengthUpRate = "PhysicalStrengthUpRate";

        public const string MagicStrengthUpRate = "MagicStrengthUpRate";

        public const string PhysicalDefenseUpRate = "PhysicalDefenseUpRate";

        public const string MagicDefenseUpRate = "MagicDefenseUpRate";
        
        public const string RecoveryUpRate = "RecoveryUpRate";

    }
}
