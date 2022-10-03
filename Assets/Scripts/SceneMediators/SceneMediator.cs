using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public static class SceneMediator
    {
        private static ISceneArgument argument;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeOnLoadMethod()
        {
            argument = null;
        }

        public static void SetArgument(ISceneArgument _argument)
        {
            argument = _argument;
        }

        public static T GetArgument<T>() where T : class, ISceneArgument
        {
            var result = argument as T;
            Assert.IsNotNull(result, $"{typeof(T)}に変換できませんでした");

            return result;
        }

        public static bool IsMatchArgument<T>() where T : class, ISceneArgument
        {
            return argument is T;
        }
    }
}
