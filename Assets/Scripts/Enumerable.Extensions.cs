using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions
    {
        public static int GetAllAttributeValue(this IEnumerable<PassiveSkill> self, string attributeName)
        {
            return self
                .Select(x => x.attributes)
                .Where(x => x.Contains(attributeName))
                .Sum(attributes => attributes.Sum(x => x.value));
        }

        public static bool Contains(this IEnumerable<PassiveSkill> self, string attributeName)
        {
            return self
                .Select(x => x.attributes)
                .Any(x => x.Contains(attributeName));
        }

        public static List<Attribute> GetAllAttributes(this IEnumerable<PassiveSkill> self, string attributeName)
        {
            return self
                .SelectMany(x => x.attributes)
                .Where(x => x.name == attributeName)
                .ToList();
        }

        public static BattleSpeedType GetNext(this BattleSpeedType self)
        {
            switch (self)
            {
                case BattleSpeedType.Lv_1:
                    return BattleSpeedType.Lv_2;
                case BattleSpeedType.Lv_2:
                    return BattleSpeedType.Lv_3;
                case BattleSpeedType.Lv_3:
                    return BattleSpeedType.Lv_1;
                default:
                    Assert.IsTrue(false, $"{self}は未対応です");
                    return default;
            }
        }

        public static string GetMessage(this BattleSpeedType self)
        {
            switch (self)
            {
                case BattleSpeedType.Lv_1:
                    return ">";
                case BattleSpeedType.Lv_2:
                    return ">>";
                case BattleSpeedType.Lv_3:
                    return ">>>";
                default:
                    Assert.IsTrue(false, $"{self}は未対応です");
                    return default;
            }
        }
    }
}
