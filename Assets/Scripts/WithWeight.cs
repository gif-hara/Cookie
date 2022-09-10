using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// <see cref="T"/>に対して重みを付与するクラス
    /// </summary>
    [Serializable]
    public abstract class WithWeight<T> : IWeight
    {
        public T value;
        
        public int weight;

        public int Weight => this.weight;
    }

    [Serializable]
    public class IntWithWeight : WithWeight<int>
    {
    }
}
