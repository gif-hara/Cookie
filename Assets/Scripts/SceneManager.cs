using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SceneManager
    {
        public static void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        
        public static UniTask LoadSceneAsync(string sceneName)
        {
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName).ToUniTask();
        }
    }
}
