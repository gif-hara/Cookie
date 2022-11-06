using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class AssetLoader
    {
        public static async UniTask<T> LoadAsync<T>(string path)
        {
            return await Addressables.LoadAssetAsync<T>(path);
        }
    }
}
