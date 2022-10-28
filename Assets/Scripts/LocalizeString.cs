using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization.Settings;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class LocalizeString
    {
        public static string Get(string tableName, string entryName)
        {
            var table = LocalizationSettings.StringDatabase.GetTable(tableName);
            var entry = table.GetEntry(entryName);
            return entry.Value;
        }
    }
}
