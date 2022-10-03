using System.Collections.Generic;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorStatus
    {
        public int hitPoint;

        public int hitPointMax;

        public int physicalStrength;

        public int magicStrength;

        public int physicalDefense;
        
        public int magicDefense;

        public int speed;

        public List<ActiveSkill> activeSkills; 

        public bool IsDead => this.hitPoint <= 0;
    }
}
