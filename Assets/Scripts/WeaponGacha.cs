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

        public List<InstanceRangeParameterWithWeight> physicalStrengths;

        public List<InstanceRangeParameterWithWeight> magicStrengths;

        public List<InstanceRangeParameterWithWeight> skillNumbers;
        
        public List<InstanceParameterWithWeight> activeSkillIds;

        public int Id => this.id;

        public string Name => this.nameKey.GetLocalizedString();
    }
}
