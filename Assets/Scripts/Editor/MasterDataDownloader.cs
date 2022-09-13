using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class MasterDataDownloader
    {
        [MenuItem("HK/Cookie/Test")]
        private static async void DownloadMasterDataPlayerStatus()
        {
            var test = await DownloadFromSpreadSheet("Player");
            var json = JsonUtility.FromJson<PlayerStatus.Json>(test);
            var masterDataPlayerStatus = AssetDatabase.LoadAssetAtPath<MasterDataPlayerStatus>("Assets/MasterData/MasterDataPlayerStatus.asset");
            masterDataPlayerStatus.playerStatusList.Clear();
            masterDataPlayerStatus.playerStatusList.AddRange(json.elements);
            EditorUtility.SetDirty(masterDataPlayerStatus);
            AssetDatabase.SaveAssets();
        }
        
        private static async UniTask<string> DownloadFromSpreadSheet(string sheetName)
        {
            var sheetUrl = File.ReadAllText("masterdata_sheet_url.txt");
            var bearer = File.ReadAllText("bearer.txt");
            var request = UnityWebRequest.Get($"{sheetUrl}?mode={sheetName}");
            request.SetRequestHeader("Authorization", $"Bearer {bearer}");
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await UniTask.Delay(100);
            }

            var result = operation.webRequest.downloadHandler.text;
            Debug.Log(result);

            return result;
        }
    }
}
