using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class Calculator
    {
        public static int GetDamage(ActorStatus attacker, ActiveSkill attackerActiveSkill, ActorStatus defenser)
        {
            var attackAttribute = (AttackAttribute)attackerActiveSkill.attributes.Get(SkillAttributeName.AttackAttribute).value;
            var defense = attackAttribute == AttackAttribute.Physical ? defenser.physicalDefense : defenser.magicDefense;
            var result = GetPower(attacker.physicalStrength, attacker.magicStrength, attackerActiveSkill) - (((defense / 2) * (defense / 2)) / 33);

            return result;
        }

        public static int GetPower(int physicalStrength, int magicStrength, ActiveSkill activeSkill)
        {
            var attackAttribute = (AttackAttribute)activeSkill.attributes.Get(SkillAttributeName.AttackAttribute).value;
            var power = activeSkill.attributes.Get(SkillAttributeName.Power).value;
            const int rate = 33;

            switch (attackAttribute)
            {
                case AttackAttribute.Physical:
                    return (physicalStrength * power) / rate;
                case AttackAttribute.Magic:
                    return (magicStrength * power) / rate;
                default:
                    Assert.IsTrue(false, $"{attackAttribute}は未対応です");
                    return 0;
            }
        }
    }
}
