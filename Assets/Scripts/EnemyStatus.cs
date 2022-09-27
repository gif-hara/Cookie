using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class EnemyStatus
    {
        public int id;

        public int hitPoint;

        public int physicalStrength;

        public int magicStrength;

        public int physicalDefense;

        public int magicDefense;

        public int speed;

        public int money;

        [Serializable]
        public class Json
        {
            public List<EnemyStatus> elements;
        }
    }
}
