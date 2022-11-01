using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ArmorGacha : IIdHolder
    {
        public LocalizedString nameKey;
        
        public int id;

        public int money;

        public List<InstanceRangeParameterWithWeight> hitPoints;

        public List<InstanceRangeParameterWithWeight> physicalDefenses;
        
        public List<InstanceRangeParameterWithWeight> magicDefenses;

        public List<InstanceRangeParameterWithWeight> speeds;
        
        public int Id => this.id;

        public string Name => this.nameKey.GetLocalizedString();
    }
    
    [Serializable]
    public sealed class ArmorGachaSpec
    {
        public int id;

        public string nameKey;

        public int money;

        [Serializable]
        public class Json
        {
            public List<ArmorGachaSpec> elements;
        }
    }
    
    [Serializable]
    public sealed class ArmorGachaPhysicalDefense
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
            public List<ArmorGachaPhysicalDefense> elements;
        }
    }
    
    [Serializable]
    public sealed class ArmorGachaMagicDefense
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
            public List<ArmorGachaMagicDefense> elements;
        }
    }
    
    [Serializable]
    public sealed class ArmorGachaHitPoint
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
            public List<ArmorGachaHitPoint> elements;
        }
    }
    
    [Serializable]
    public sealed class ArmorGachaSpeed
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
            public List<ArmorGachaSpeed> elements;
        }
    }
}
