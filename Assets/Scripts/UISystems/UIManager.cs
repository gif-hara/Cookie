using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform uiParent;
        
        public static UIManager Instance { get; private set; }
        
        public static async UniTask Setup()
        {
            Assert.IsNull(Instance);
            
            var prefab = await AssetLoader.LoadAsync<GameObject>("Assets/Prefabs/UI/UIManager.prefab");
            Instance = Instantiate(prefab).GetComponent<UIManager>();
            DontDestroyOnLoad(Instance);
        }

        public static T Open<T>(T uiView) where T : UIView
        {
            return Instantiate(uiView, Instance.uiParent);
        }
    }
}
