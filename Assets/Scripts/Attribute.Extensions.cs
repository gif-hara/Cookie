using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        public static Attribute Get(this IEnumerable<Attribute> self, string name)
        {
            var result = self.FirstOrDefault(x => x.name == name);
            Assert.IsNotNull(result, $"{name}に対応する{typeof(Attribute)}は存在しません");

            return result;
        }
    }
}
