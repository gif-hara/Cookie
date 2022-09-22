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

        public List<InstanceRangeParameterWithWeight> hitPoints;

        public List<InstanceRangeParameterWithWeight> physicalDefenses;
        
        public List<InstanceRangeParameterWithWeight> magicDefenses;

        public List<InstanceRangeParameterWithWeight> speeds;
        
        public int Id => this.id;

        public string Name => this.nameKey.GetLocalizedString();
    }
}
