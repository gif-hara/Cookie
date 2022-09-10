using System;
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

        public int hitPointMin;

        public int hitPointMax;

        public int physicalDefenseMin;

        public int physicalDefenseMax;
        
        public int magicDefenseMin;

        public int magicDefenseMax;

        public int speedMin;

        public int speedMax;
        
        public int Id => this.id;

        public string Name => this.nameKey.GetLocalizedString();
    }
}
