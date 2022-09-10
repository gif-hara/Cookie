using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// <see cref="IIdHolder"/>に関する拡張関数
    /// </summary>
    public static class IIdHolderExtensions
    {
        public static T Find<T>(this IEnumerable<T> self, int id) where T : class, IIdHolder
        {
            var result = self.FirstOrDefault(x => x.Id == id);
            Assert.IsNotNull(result, $"{nameof(id)} = {id}に対応する{typeof(T)}が存在しません");

            return result;
        }
    }
}
