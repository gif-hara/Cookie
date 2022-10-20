using System;
using System.Collections.Generic;
using System.Linq;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ActorStatusBuilder
    {
        public int hitPoint;

        public int physicalStrength;
        
        public int magicStrength;

        public int physicalDefense;

        public int magicDefense;

        public int speed;

        public List<int> activeSkillIds;

        public List<int> passiveSkillIds;

        public ActorStatus Create()
        {
            return new ActorStatus
            {
                hitPoint = this.hitPoint,
                hitPointMax = this.hitPoint,
                physicalStrength = this.physicalStrength,
                magicStrength = this.magicStrength,
                physicalDefense = this.physicalDefense,
                magicDefense = this.magicDefense,
                speed = this.speed,
                activeSkills = this.activeSkillIds.Select(x => MasterDataActiveSkill.Instance.skills.Find(y => y.id == x)).ToList(),
                passiveSkills = this.passiveSkillIds.Select(x => MasterDataPassiveSkill.Instance.skills.Find(y => y.id == x)).ToList()
            };
        }
    }
}
