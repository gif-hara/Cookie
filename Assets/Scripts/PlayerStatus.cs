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
    public sealed class PlayerStatus
    {
        public int Level;

        public int Strength;

        public int Defense;

        public int Power;
        
        public int HitPoint;

        public int Speed;
        
        [Serializable]
        public class Json
        {
            public List<PlayerStatus> elements;
        }
    }
}
