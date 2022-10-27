using System;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// シーンを制御するクラス
    /// </summary>
    public abstract class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }

        private MessageBroker messageBroker;

        public MessageBroker MessageBroker
        {
            get
            {
                if (this.messageBroker == null)
                {
                    Debug.LogWarning($"{this.GetType()}は{typeof(MessageBroker)}のセットアップを完了していません");
                    this.messageBroker = new MessageBroker(null);
                }

                return this.messageBroker;
            }
        }

        private readonly DisposableBagBuilder sceneScope = DisposableBag.CreateBuilder();

        private void Awake()
        {
            Instance = this;
            this.messageBroker = new MessageBroker(OnInitializeMessageBroker);
        }
        
        async void Start()
        {
            await BootSystem.IsReady;
            await OnStartAsync(this.sceneScope);
        }

        private void OnDestroy()
        {
            this.sceneScope.Build().Dispose();
            OnDestroyInternal();
            Instance = null;
        }

        protected virtual UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            return UniTask.CompletedTask;
        }

        protected virtual void OnDestroyInternal()
        {
        }

        protected virtual void OnInitializeMessageBroker(BuiltinContainerBuilder builder)
        {
        }
    }
}
