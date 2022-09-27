using System;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SelectEnemySceneController : SceneController
    {
        [SerializeField]
        private Transform uiParent;

        [SerializeField]
        private GachaUIView gachaUIPrefab;

        private CookieButton selectedRootButton;

        private CookieButton selectedGachaButton;

        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            return base.OnStartAsync(scope);
        }
        
        protected override void OnInitializeMessageBroker(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<GachaEvent.RequestWeaponGacha>();
            builder.AddMessageBroker<GachaEvent.RequestArmorGacha>();
            builder.AddMessageBroker<GachaEvent.RequestAccessoryGacha>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log(JsonUtility.ToJson(UserData.current, true));
            }
        }
    }
}
