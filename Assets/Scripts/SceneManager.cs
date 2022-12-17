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
            LoadSceneAsyncInternal(sceneName).Forget();
        }
        
        public static UniTask LoadSceneAsync(string sceneName)
        {
            return LoadSceneAsyncInternal(sceneName);
        }

        private static async UniTask LoadSceneAsyncInternal(string sceneName)
        {
            await MessageBroker.Instance.GetAsyncPublisher<GlobalEvent.WillChangeScene>()
                .PublishAsync(GlobalEvent.WillChangeScene.Get());
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName).ToUniTask();
            await MessageBroker.Instance.GetAsyncPublisher<GlobalEvent.ChangedScene>()
                .PublishAsync(GlobalEvent.ChangedScene.Get());
        }
    }
}
