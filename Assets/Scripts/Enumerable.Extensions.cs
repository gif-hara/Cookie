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
    }
}
