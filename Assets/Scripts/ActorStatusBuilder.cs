using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

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

        public List<int> activeSkillIds = new ();

        public List<int> passiveSkillIds = new ();

        public ActorStatus Create()
        {
            return new ActorStatus
            {
                hitPoint = new AsyncReactiveProperty<int>(this.hitPoint),
                hitPointMax = new AsyncReactiveProperty<int>(this.hitPoint),
                physicalStrength = this.physicalStrength,
                magicStrength = this.magicStrength,
                physicalDefense = this.physicalDefense,
                magicDefense = this.magicDefense,
                speed = this.speed,
                activeSkills = this.activeSkillIds.Select(x => MasterDataActiveSkill.Instance.skills.Find(y => y.id == x)).ToList(),
                passiveSkills = this.passiveSkillIds.Select(x => MasterDataPassiveSkill.Instance.skills.Find(y => y.id == x)).ToList(),
                abnormalStatuses = new HashSet<AbnormalStatus>()
            };
        }
    }
}
