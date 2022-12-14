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
        
        // シリアライズするからstringのままにしておく
        public string nameKey;

        public InstanceParameter physicalStrength;

        public InstanceParameter magicStrength;
        
        public InstanceParameter criticalRate;

        public List<InstanceParameter> activeSkillIds = new();

        public string Name => LocalizationSettings.StringDatabase.GetTable("Weapon").GetEntry(this.nameKey).Value;

        /// <summary>
        /// 総合攻撃力を返す
        /// </summary>
        public int TotalStrength
        {
            get
            {
                var result = this.activeSkillIds
                    .Select(x => MasterDataActiveSkill.Instance.skills.Find(x.parameter))
                    .Where(x => x.attributes.Contains(SkillAttributeName.AttackPower))
                    .Sum(x => Calculator.GetAttackPower(this.physicalStrength.parameter, this.magicStrength.parameter, x));

                var criticalDamage = result * (Define.CriticalDamageRate - 1.0f) * (this.criticalRate.parameter / 100.0f);
                
                // 相手の防御力が100と仮定した際のダメージを総合攻撃力とする
                result = Mathf.FloorToInt((result + criticalDamage) / 100.0f);

                return result;
            }
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}
