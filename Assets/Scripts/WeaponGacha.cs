using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class WeaponGacha : IIdHolder
    {
        public LocalizedString nameKey;
        
        public int id;

        public int physicalStrengthMin;

        public int physicalStrengthMax;
        
        public int magicStrengthMin;

        public int magicStrengthMax;
        
        /// <summary>
        /// 付与できるスキルの数の最小値
        /// </summary>
        public int skillNumberMin;

        /// <summary>
        /// 付与できるスキルの数の最大値
        /// </summary>
        public int skillNumberMax;

        public List<InstanceParameterWithWeight> activeSkillIds;

        public int Id => this.id;

        public string Name => this.nameKey.GetLocalizedString();
    }
}
