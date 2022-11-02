using System;
using UnityEngine.Localization.Settings;

namespace Cookie
{
    /// <summary>
    /// 防具
    /// </summary>
    [Serializable]
    public sealed class Armor
    {
        public int instanceId;
        
        // シリアライズするからstringのままにしておく
        public string nameKey;

        public int hitPoint;

        public int physicalDefense;

        public int magicDefense;

        public int speed;

        public string Name => LocalizationSettings.StringDatabase.GetTable("Armor").GetEntry(this.nameKey).Value;
    }
}
