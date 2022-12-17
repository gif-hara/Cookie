using Cysharp.Threading.Tasks;
using MessagePipe;
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

        [SerializeField]
        private StartMenuUIView startMenuUIViewPrefab;

        [SerializeField]
        private NotifyUIView notifyUIViewPrefab;

        [SerializeField]
        private PopupUIView popupUIViewPrefab;

        [SerializeField]
        private FadeUIView fadeUIViewPrefab;

        private StartMenuUIController startMenuUIController;

        private NotifyUIController notifyUIController;

        private PopupUIController popupUIController;

        private FadeUIController fadeUIController;
        
        public static UIManager Instance { get; private set; }

        public static StartMenuUIController StartMenuUIController => Instance.startMenuUIController;

        public static NotifyUIController NotifyUIController => Instance.notifyUIController;

        public static PopupUIController PopupUIController => Instance.popupUIController;

        public static FadeUIController FadeUIController => Instance.fadeUIController;
        
        public static async UniTask Setup(DisposableBagBuilder bag)
        {
            Assert.IsNull(Instance);
            
            var prefab = await AssetLoader.LoadAsync<GameObject>("Assets/Prefabs/UI/UIManager.prefab");
            Instance = Instantiate(prefab).GetComponent<UIManager>();
            DontDestroyOnLoad(Instance);

            Instance.startMenuUIController = new StartMenuUIController();
            Instance.startMenuUIController.Setup(Instance.startMenuUIViewPrefab);

            Instance.notifyUIController = new NotifyUIController();
            Instance.notifyUIController.Setup(Instance.notifyUIViewPrefab);

            Instance.popupUIController = new PopupUIController();
            Instance.popupUIController.Setup(Instance.popupUIViewPrefab);

            Instance.fadeUIController = new FadeUIController();
            Instance.fadeUIController.Setup(Instance.fadeUIViewPrefab, bag);
        }

        public static T Open<T>(T uiView) where T : UIView
        {
            return Instantiate(uiView, Instance.uiParent);
        }

        public static void Close(UIView uiView)
        {
            Destroy(uiView.gameObject);
        }

        public static void Show(UIView uiView)
        {
            uiView.gameObject.SetActive(true);
        }
        
        public static void Hidden(UIView uiView)
        {
            uiView.gameObject.SetActive(false);
        }

        public static void SetAsLastSibling(UIView uiView)
        {
            uiView.transform.SetAsLastSibling();
        }
    }
}
