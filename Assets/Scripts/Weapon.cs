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

        public int physicalStrength;

        public int magicStrength;

        public List<int> activeSkillIds = new();

        public string Name => LocalizationSettings.StringDatabase.GetTable("Weapon").GetEntry(this.nameKey).Value;

        /// <summary>
        /// 総合攻撃力を返す
        /// </summary>
        public int TotalStrength
        {
            get
            {
                return this.activeSkillIds
                    .Select(x => MasterDataActiveSkill.Instance.skills.Find(x))
                    .Where(x => x.skillType == ActiveSkillType.Attack)
                    .Sum(x => Calculator.GetPower(this.physicalStrength, this.magicStrength, x));
            }
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}
