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
    public sealed class AccessoryGacha : IIdHolder
    {
        public LocalizedString nameKey;
        
        public int id;
        
        /// <summary>
        /// 付与できるスキルの数の最小値
        /// </summary>
        public int skillNumberMin;

        /// <summary>
        /// 付与できるスキルの数の最大値
        /// </summary>
        public int skillNumberMax;

        public List<IntWithWeight> passiveSkillIds;

        public int Id => this.id;

        public string Name => this.nameKey.GetLocalizedString();
    }
}
