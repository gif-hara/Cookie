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
        
        public List<InstanceRangeParameterWithWeight> criticalRates;

        public List<InstanceRangeParameterWithWeight> skillNumbers;
        
        public List<InstanceParameterWithWeight> activeSkillIds;

        public int Id => this.id;

        public string Name => this.nameKey.GetLocalizedString();
    }

    [Serializable]
    public sealed class WeaponGachaSpec
    {
        public int id;

        public string nameKey;

        [Serializable]
        public class Json
        {
            public List<WeaponGachaSpec> elements;
        }
    }

    [Serializable]
    public sealed class WeaponGachaPhysicalStrength
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
            public List<WeaponGachaPhysicalStrength> elements;
        }
    }

    [Serializable]
    public sealed class WeaponGachaMagicStrength
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
            public List<WeaponGachaMagicStrength> elements;
        }
    }

    [Serializable]
    public sealed class WeaponGachaCriticalRate
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
            public List<WeaponGachaMagicStrength> elements;
        }
    }

    [Serializable]
    public sealed class WeaponGachaSkillNumber
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
            public List<WeaponGachaSkillNumber> elements;
        }
    }

    [Serializable]
    public sealed class WeaponGachaActiveSkill
    {
        public int id;

        public int gachaId;

        public int value;

        public Rare rare;

        public int weight;
        
        [Serializable]
        public class Json
        {
            public List<WeaponGachaActiveSkill> elements;
        }
    }
}
