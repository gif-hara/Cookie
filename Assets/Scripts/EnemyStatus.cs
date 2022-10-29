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
        
        public int criticalRate;

        public int physicalDefense;

        public int magicDefense;

        public int speed;

        public int money;

        public int fieldId;

        public int spriteId;

        /// <summary>
        /// プレイヤーレベル
        /// レベルデザイン用に持っている
        /// </summary>
        public int playerLevel;

        public List<int> activeSkills;

        public List<DefeatEnemyUnlock> defeatEnemyUnlocks;

        public string Name => this.nameKey.GetLocalizedString();

        public ActorStatusBuilder ToActorStatusBuilder()
        {
            return new ActorStatusBuilder
            {
                nameKey = this.nameKey,
                hitPoint = this.hitPoint,
                physicalStrength = this.physicalStrength,
                magicStrength = this.magicStrength,
                criticalRate = this.criticalRate,
                physicalDefense = this.physicalDefense,
                magicDefense = this.magicDefense,
                speed = this.speed,
                activeSkillIds = activeSkills,
                spriteId = this.spriteId
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

        public int fieldId;

        public int playerLevel;

        [Serializable]
        public class Json
        {
            public List<EnemySpec> elements;
        }
    }
    
    [Serializable]
    public sealed class EnemyActiveSkill
    {
        public int id;

        public int enemyId;

        public int activeSkillId;

        [Serializable]
        public class Json
        {
            public List<EnemyActiveSkill> elements;
        }
    }

    [Serializable]
    public sealed class DefeatEnemyUnlock
    {
        public int id;

        public int enemyId;

        public UnlockType unlockType;

        public int unlockId;

        [Serializable]
        public class Json
        {
            public List<DefeatEnemyUnlock> elements;
        }
    }
}
