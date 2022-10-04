using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class Calculator
    {
        public static int GetDamage(ActorStatus attacker, ActiveSkill attackerActiveSkill, ActorStatus target)
        {
            var attackAttribute = (AttackAttribute)attackerActiveSkill.attributes.Get(SkillAttributeName.AttackAttribute).value;
            var defense = attackAttribute == AttackAttribute.Physical ? target.physicalDefense : target.magicDefense;
            var result = GetPower(attacker.physicalStrength, attacker.magicStrength, attackerActiveSkill) / defense;

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
    }
}
