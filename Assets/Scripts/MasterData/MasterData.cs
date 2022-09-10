using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MasterData<T> : ScriptableObject where T : MasterData<T>
    {
        public static T Instance { get; private set; }

        public static async UniTask LoadAsync(string path)
        {
            var result = await AssetLoader.LoadAsync<T>(path);
            Instance = result;
        }

        public static void Clear()
        {
            Instance = null;
        }
    }
}
