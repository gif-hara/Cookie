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
            var result = GetPower(physicalStrength, magicStrength, attackerActiveSkill) / defense;

            return result;
        }

        public static int GetPower(int physicalStrength, int magicStrength, ActiveSkill activeSkill)
        {
            var attackAttribute = (AttackAttribute)activeSkill.attributes.Get(SkillAttributeName.AttackAttribute).value;
            var power = activeSkill.attributes.Get(SkillAttributeName.Power).value;

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
    }
}
