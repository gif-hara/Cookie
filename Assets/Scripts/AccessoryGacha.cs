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
        /// 付与できるスキルの数
        /// </summary>
        public List<InstanceRangeParameterWithWeight> skillNumbers;

        public List<InstanceParameterWithWeight> passiveSkillIds;

        public int Id => this.id;

        public string Name => this.nameKey.GetLocalizedString();
    }
    
        
    [Serializable]
    public sealed class AccessoryGachaSpec
    {
        public int id;

        public string nameKey;

        [Serializable]
        public class Json
        {
            public List<AccessoryGachaSpec> elements;
        }
    }
    
    [Serializable]
    public sealed class AccessoryGachaSkillNumber
    {
        public int id;

        public int gachaId;

        public int min;

        public int max;

        public Rare rare;

        public int weight;
        
        [Serializable]
        public class Json
        {
            public List<AccessoryGachaSkillNumber> elements;
        }
    }
    
    [Serializable]
    public sealed class AccessoryGachaPassiveSkill
    {
        public int id;

        public int gachaId;

        public int value;

        public Rare rare;

        public int weight;
        
        [Serializable]
        public class Json
        {
            public List<AccessoryGachaPassiveSkill> elements;
        }
    }
}
