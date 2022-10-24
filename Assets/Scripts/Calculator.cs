using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class Calculator
    {
        public static int GetDamage(
            ActorStatus attacker,
            ActiveSkill attackerActiveSkill,
            ActorStatus target
            )
        {
            var attackAttribute = (AttackAttribute)attackerActiveSkill.attributes.Get(SkillAttributeName.AttackAttribute).value;
            var physicalStrength = Mathf.FloorToInt(attacker.physicalStrength * GetPhysicalStrengthUpRate(attacker.passiveSkills));
            var magicStrength = Mathf.FloorToInt(attacker.magicStrength * GetMagicStrengthUpRate(attacker.passiveSkills));
            var defense = attackAttribute == AttackAttribute.Physical ? target.physicalDefense : target.magicDefense;
            var defenseUpRate = attackAttribute == AttackAttribute.Physical ? GetPhysicalDefenseUpRate(target.passiveSkills) : GetMagicDefenseUpRate(target.passiveSkills);
            defense = Mathf.FloorToInt(defense * defenseUpRate);
            var result = GetAttackPower(physicalStrength, magicStrength, attackerActiveSkill) / defense;

            if (attacker.abnormalStatuses.Contains(AbnormalStatus.Debility))
            {
                result = Mathf.FloorToInt(result * (2.0f / 3.0f));
            }

            if (target.abnormalStatuses.Contains(AbnormalStatus.Fragility))
            {
                result = Mathf.FloorToInt(result * (4.0f / 3.0f));
            }

            return result;
        }

        public static int GetAttackPower(int physicalStrength, int magicStrength, ActiveSkill activeSkill)
        {
            var attackAttribute = (AttackAttribute)activeSkill.attributes.Get(SkillAttributeName.AttackAttribute).value;
            var power = activeSkill.attributes.Get(SkillAttributeName.AttackPower).value;

            switch (attackAttribute)
            {
                case AttackAttribute.Physical:
                    return physicalStrength * power;
                case AttackAttribute.Magic:
                    return magicStrength * power;
                default:
                    Assert.IsTrue(false, $"{attackAttribute}は未対応です");
                    return 0;
            }
        }

        public static float GetPhysicalStrengthUpRate(IEnumerable<PassiveSkill> passiveSkills)
        {
            return 1.0f + ((float)passiveSkills.GetAllAttributeValue(SkillAttributeName.PhysicalStrengthUpRate) / 100);
        }

        public static float GetMagicStrengthUpRate(IEnumerable<PassiveSkill> passiveSkills)
        {
            return 1.0f + ((float)passiveSkills.GetAllAttributeValue(SkillAttributeName.MagicStrengthUpRate) / 100);
        }

        public static float GetPhysicalDefenseUpRate(IEnumerable<PassiveSkill> passiveSkills)
        {
            return 1.0f + ((float)passiveSkills.GetAllAttributeValue(SkillAttributeName.PhysicalDefenseUpRate) / 100);
        }

        public static float GetMagicDefenseUpRate(IEnumerable<PassiveSkill> passiveSkills)
        {
            return 1.0f + ((float)passiveSkills.GetAllAttributeValue(SkillAttributeName.MagicDefenseUpRate) / 100);
        }

        /// <summary>
        /// 回復力UPの倍率を返す
        /// </summary>
        public static float GetRecoveryUpRate(IEnumerable<PassiveSkill> passiveSkills)
        {
            return 1.0f + ((float)passiveSkills.GetAllAttributeValue(SkillAttributeName.RecoveryUpRate) / 100);
        }
        
        /// <summary>
        /// 回復量を返す
        /// </summary>
        public static float GetRecoveryRate(ActorStatus actorStatus, ActiveSkill activeSkill)
        {
            var result = (float)activeSkill.attributes.Get(SkillAttributeName.RecoveryPower).value / 100;
            result *= GetRecoveryUpRate(actorStatus.passiveSkills);

            return result;
        }

        public static bool CanAddAbnormalStatus(AbnormalStatus abnormalStatus, ActorStatus targetStatus)
        {
            // 状態異常を無効化するパッシブスキルを持っていた場合は付与できない
            var attributes = targetStatus.passiveSkills.GetAllAttributes(SkillAttributeName.InvalidateAbnormalStatus);
            if (attributes.FindIndex(x => (AbnormalStatus)x.value == abnormalStatus) >= 0)
            {
                return false;
            }
            
            return Random.value > 0.5f;
        }

        public static int GetPoisonDamage(ActorStatus actorStatus)
        {
            return actorStatus.hitPointMax / 5;
        }

        public static bool CanInvokeParalysis()
        {
            return Random.value > 0.66f;
        }
    }
}
