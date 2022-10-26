using System.Collections.Generic;
using System.Linq;
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

        public int criticalRate;

        public int physicalDefense;
        
        public int magicDefense;

        public int speed;

        public int physicalStrengthBuffLevel;

        public int magicStrengthBuffLevel;

        public int physicalDefenseBuffLevel;

        public int magicDefenseBuffLevel;

        public List<ActiveSkill> activeSkills;

        public List<PassiveSkill> passiveSkills;

        public HashSet<AbnormalStatus> abnormalStatuses;

        public bool IsDead => this.hitPoint <= 0;
    }
}
