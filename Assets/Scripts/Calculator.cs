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
        public static DamageData GetDamageData(
            ActorStatus attacker,
            ActiveSkill attackerActiveSkill,
            ActorStatus target
            )
        {
            var result = new DamageData();
            result.attackAttribute = (AttackAttribute)attackerActiveSkill.attributes.Get(SkillAttributeName.AttackAttribute).value;
            
            var physicalStrength = attacker.physicalStrength;
            physicalStrength = Mathf.FloorToInt(
                physicalStrength *
                (1.0f + GetPhysicalStrengthUpRateFromPassiveSkill(attacker.passiveSkills) + GetStrengthUpRateFromBuff(attacker.physicalStrengthBuffLevel))
                );
            
            var magicStrength = attacker.magicStrength;
            magicStrength = Mathf.FloorToInt(
                magicStrength *
                (1.0f + GetMagicStrengthUpRateFromPassiveSkill(attacker.passiveSkills) + GetStrengthUpRateFromBuff(attacker.magicStrengthBuffLevel))
                );
            
            var defense = result.attackAttribute == AttackAttribute.Physical ? target.physicalDefense : target.magicDefense;
            var defenseUpRateFromPassiveSkill = result.attackAttribute == AttackAttribute.Physical
                ? GetPhysicalDefenseUpRateFromPassiveSkill(target.passiveSkills)
                : GetMagicDefenseUpRateFromPassiveSkill(target.passiveSkills);
            var defenseUpRateFromBuff = result.attackAttribute == AttackAttribute.Physical
                ? GetDefenseUpRateFromBuff(target.physicalDefenseBuffLevel)
                : GetDefenseUpRateFromBuff(target.magicDefenseBuffLevel);
            defense = Mathf.FloorToInt(
                defense *
                (1.0f + defenseUpRateFromPassiveSkill + defenseUpRateFromBuff)
                );
            
            var criticalRate = attacker.criticalRate + attacker.passiveSkills.GetAllAttributeValue(SkillAttributeName.CriticalRateUpFixed);
            result.isCritical = IsCritical(criticalRate);
            var critical = result.isCritical ? 1.5f : 1.0f;
            result.damage = Mathf.FloorToInt(GetAttackPower(physicalStrength, magicStrength, attackerActiveSkill) * critical) / defense;

            if (attacker.abnormalStatuses.Contains(AbnormalStatus.Debility))
            {
                result.damage = Mathf.FloorToInt(result.damage * (2.0f / 3.0f));
            }

            if (target.abnormalStatuses.Contains(AbnormalStatus.Fragility))
            {
                result.damage = Mathf.FloorToInt(result.damage * (4.0f / 3.0f));
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

        public static float GetPhysicalStrengthUpRateFromPassiveSkill(IEnumerable<PassiveSkill> passiveSkills)
        {
            return (float)passiveSkills.GetAllAttributeValue(SkillAttributeName.PhysicalStrengthUpRate) / 100;
        }

        public static float GetMagicStrengthUpRateFromPassiveSkill(IEnumerable<PassiveSkill> passiveSkills)
        {
            return (float)passiveSkills.GetAllAttributeValue(SkillAttributeName.MagicStrengthUpRate) / 100;
        }

        public static float GetPhysicalDefenseUpRateFromPassiveSkill(IEnumerable<PassiveSkill> passiveSkills)
        {
            return (float)passiveSkills.GetAllAttributeValue(SkillAttributeName.PhysicalDefenseUpRate) / 100;
        }

        public static float GetMagicDefenseUpRateFromPassiveSkill(IEnumerable<PassiveSkill> passiveSkills)
        {
            return (float)passiveSkills.GetAllAttributeValue(SkillAttributeName.MagicDefenseUpRate) / 100;
        }

        public static float GetStrengthUpRateFromBuff(int buffLevel)
        {
            var results = new[]
            {
                0.0f,
                0.25f,
                0.5f,
                0.75f,
                1.0f
            };

            return results[buffLevel];
        }

        public static float GetDefenseUpRateFromBuff(int buffLevel)
        {
            var results = new[]
            {
                0.0f,
                0.25f,
                0.5f,
                0.75f,
                1.0f
            };

            return results[buffLevel];
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

        public static bool CanAddAbnormalStatus(AbnormalStatus abnormalStatus, ActorStatus attackerStatus, ActorStatus targetStatus)
        {
            // 状態異常を無効化するパッシブスキルを持っていた場合は付与できない
            var attributes = targetStatus.passiveSkills.GetAllAttributes(SkillAttributeName.InvalidateAbnormalStatus);
            if (attributes.FindIndex(x => (AbnormalStatus)x.value == abnormalStatus) >= 0)
            {
                return false;
            }

            // 既に状態異常にかかっている場合はかからない
            if (targetStatus.abnormalStatuses.Contains(abnormalStatus))
            {
                return false;
            }

            var rate = 0.5f +
                attackerStatus.passiveSkills.GetAllAttributeValue(SkillAttributeName.AddAbnormalStatusRateUpFixed) / 100.0f;
            
            return Random.value <= rate;
        }

        public static DamageData GetPoisonDamage(ActorStatus actorStatus)
        {
            return new DamageData
            {
                damage = Mathf.FloorToInt((float)actorStatus.hitPointMax / 5),
                attackAttribute = AttackAttribute.Physical,
                isCritical = false
            };
        }

        public static bool CanInvokeParalysis()
        {
            return Random.value <= 0.66f;
        }

        public static bool IsCritical(int criticalRate)
        {
            return Random.value <= ((float)criticalRate / 100);
        }

        /// <summary>
        /// 反撃によるダメージを返す
        /// </summary>
        public static DamageData GetCounterDamage(int damage, AttackAttribute attackAttribute)
        {
            return new DamageData
            {
                damage = Mathf.FloorToInt(damage * 0.2f),
                attackAttribute = attackAttribute,
                isCritical = false
            };
        }

        /// <summary>
        /// 吸収による回復量を返す
        /// </summary>
        public static int GetAbsorptionRecoveryAmount(int damage)
        {
            return Mathf.FloorToInt(damage * 0.2f);
        }

        /// <summary>
        /// 連続攻撃を行えるか
        /// </summary>
        public static bool CanInvokeContinuousAttack(ActorStatus actorStatus)
        {
            if (!actorStatus.passiveSkills.Contains(SkillAttributeName.ContinuousAttack))
            {
                return false;
            }

            return Random.value <= 0.2f;
        }
    }
}
