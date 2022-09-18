using System;
using UnityEngine;
using UnityEngine.Assertions;

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
    }

    [Serializable]
    public sealed class InstanceParameterWithWeight : WithWeight<InstanceParameter>
    {
    }
}
