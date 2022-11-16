using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ActorStatusBuilder
    {
        public LocalizedString nameKey;
        
        public int hitPoint;

        public int physicalStrength;
        
        public int magicStrength;

        public int criticalRate;

        public int physicalDefense;

        public int magicDefense;

        public int speed;

        public int spriteId;

        public int playerLevel;

        public int appearanceAnimationId;

        public int diedAnimationId;

        public List<int> activeSkillIds = new ();

        public List<int> passiveSkillIds = new ();

        public ActorStatus Create()
        {
            return new ActorStatus
            {
                nameKey = this.nameKey,
                hitPoint = new AsyncReactiveProperty<int>(this.hitPoint),
                hitPointMax = new AsyncReactiveProperty<int>(this.hitPoint),
                physicalStrength = this.physicalStrength,
                magicStrength = this.magicStrength,
                criticalRate = this.criticalRate,
                physicalDefense = this.physicalDefense,
                magicDefense = this.magicDefense,
                speed = this.speed,
                activeSkills = this.activeSkillIds.Select(x => MasterDataActiveSkill.Instance.skills.Find(y => y.id == x)).ToList(),
                passiveSkills = this.passiveSkillIds.Select(x => MasterDataPassiveSkill.Instance.skills.Find(y => y.id == x)).ToList(),
                abnormalStatuses = new HashSet<AbnormalStatus>(),
                spriteId = this.spriteId,
                playerLevel = this.playerLevel,
                appearanceAnimationId = this.appearanceAnimationId,
                diedAnimationId = this.diedAnimationId
            };
        }
    }
}
