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

        public string Name => this.nameKey.GetLocalizedString();

        public ActorStatusBuilder ToActorStatusBuilder()
        {
            return new ActorStatusBuilder
            {
                hitPoint = this.hitPoint,
                physicalStrength = this.physicalStrength,
                magicStrength = this.magicStrength,
                physicalDefense = this.physicalDefense,
                magicDefense = this.magicDefense,
                speed = this.speed,
                activeSkillIds = new List<int>()
            };
        }
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
