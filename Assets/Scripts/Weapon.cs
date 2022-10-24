using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Cookie
{
    /// <summary>
    /// 武器
    /// </summary>
    [Serializable]
    public sealed class Weapon
    {
        public int instanceId;
        
        public string nameKey;

        public InstanceParameter physicalStrength;

        public InstanceParameter magicStrength;

        public List<InstanceParameter> activeSkillIds = new();

        public string Name => LocalizationSettings.StringDatabase.GetTable("Weapon").GetEntry(this.nameKey).Value;

        /// <summary>
        /// 総合攻撃力を返す
        /// </summary>
        public int TotalStrength
        {
            get
            {
                return this.activeSkillIds
                    .Select(x => MasterDataActiveSkill.Instance.skills.Find(x.parameter))
                    .Where(x => x.attributes.Contains(SkillAttributeName.AttackPower))
                    .Sum(x => Calculator.GetAttackPower(this.physicalStrength.parameter, this.magicStrength.parameter, x));
            }
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}
