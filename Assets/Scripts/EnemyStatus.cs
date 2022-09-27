using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class EnemyStatus
    {
        public int id;

        public LocalizedString nameKey;

        public int hitPoint;

        public int physicalStrength;

        public int magicStrength;

        public int physicalDefense;

        public int magicDefense;

        public int speed;

        public int money;
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class EnemySpec
    {
        public int id;

        public string nameKey;

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
            public List<EnemySpec> elements;
        }
    }
}
