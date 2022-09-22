using System;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;

namespace Cookie
{
    /// <summary>
    /// アクセサリー
    /// </summary>
    [Serializable]
    public sealed class Accessory
    {
        public int instanceId;
        
        public string nameKey;
        
        public List<InstanceParameter> passiveSkillIds = new();

        public string Name => LocalizationSettings.StringDatabase.GetTable("Accessory").GetEntry(this.nameKey).Value;
    }
}
