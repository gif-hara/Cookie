using System;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class InstanceParameter
    {
        public int parameter;

        public Rare rare;

        public InstanceParameter()
        {
        }

        public InstanceParameter(InstanceParameter other)
        {
            this.parameter = other.parameter;
            this.rare = other.rare;
        }
    }

    [Serializable]
    public sealed class InstanceRangeParameter
    {
        public int min;

        public int max;

        public Rare rare;

        public int GetParameter() => Random.Range(this.min, this.max + 1);
    }

    [Serializable]
    public sealed class InstanceParameterWithWeight : WithWeight<InstanceParameter>
    {
    }

    [Serializable]
    public sealed class InstanceRangeParameterWithWeight : WithWeight<InstanceRangeParameter>
    {
    }
}
