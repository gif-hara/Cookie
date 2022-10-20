using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorStatus
    {
        public AsyncReactiveProperty<int> hitPoint;

        public AsyncReactiveProperty<int> hitPointMax;

        public int physicalStrength;

        public int magicStrength;

        public int physicalDefense;
        
        public int magicDefense;

        public int speed;

        public List<ActiveSkill> activeSkills;

        public List<PassiveSkill> passiveSkills;

        public bool IsDead => this.hitPoint <= 0;
    }
}
