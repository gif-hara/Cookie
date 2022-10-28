using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

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

        private StartMenuUIView startMenuUIView;

        private NotifyUIController notifyUIController;
        
        public static UIManager Instance { get; private set; }

        public static StartMenuUIView StartMenuUIView => Instance.startMenuUIView;

        public static NotifyUIController NotifyUIController => Instance.notifyUIController;
        
        public static async UniTask Setup()
        {
            Assert.IsNull(Instance);
            
            var prefab = await AssetLoader.LoadAsync<GameObject>("Assets/Prefabs/UI/UIManager.prefab");
            Instance = Instantiate(prefab).GetComponent<UIManager>();
            DontDestroyOnLoad(Instance);

            Instance.startMenuUIView = Open(Instance.startMenuUIViewPrefab);
            Hidden(Instance.startMenuUIView);
            Instance.startMenuUIView.GachaButton.Button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Gacha");
                Hidden(StartMenuUIView);
            });
            
            Instance.startMenuUIView.EditEquipmentButton.Button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("EditEquipment");
                Hidden(StartMenuUIView);
            });
            
            Instance.startMenuUIView.SelectEnemyButton.Button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("SelectEnemy");
                Hidden(StartMenuUIView);
            });

            Instance.notifyUIController = new NotifyUIController();
            Instance.notifyUIController.Setup(Instance.notifyUIViewPrefab);
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
