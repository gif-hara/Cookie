using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class RareEffectHolder
    {
        [Serializable]
        public class Data
        {
            public Rare rare;

            public GameObject prefab;
        }

        [SerializeField]
        private List<Data> dataList;

        public GameObject Create(Rare rare)
        {
            var data = this.dataList.Find(x => x.rare == rare);
            Assert.IsNotNull(data);

            return Object.Instantiate(data.prefab);
        }
    }
}
