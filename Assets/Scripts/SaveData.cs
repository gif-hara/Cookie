using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class SaveData
    {
        private const string UserDataKey = "Cookie.UserData";
        
        public static UserData LoadUserData()
        {
            if (!ContainsUserData())
            {
                return new UserData();
            }

            return JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(UserDataKey));
            // return JsonUtility.FromJson<UserData>(HK.Framework.Systems.SaveData.GetString(UserDataKey));
        }

        public static void SaveUserData(UserData userData)
        {
            PlayerPrefs.SetString(UserDataKey, JsonUtility.ToJson(userData));
            // HK.Framework.Systems.SaveData.SetString(UserDataKey, JsonUtility.ToJson(userData));
            // HK.Framework.Systems.SaveData.Save();
        }

        public static bool ContainsUserData()
        {
            return PlayerPrefs.HasKey(UserDataKey);
            // return HK.Framework.Systems.SaveData.ContainsKey(UserDataKey);
        }
    }
}
