using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AttackDeclarationUIView : UIView
    {
        [SerializeField]
        private AttackDeclarationElement elementPrefab;
        
        [SerializeField]
        private RectTransform playerPosition;

        [SerializeField]
        private RectTransform enemyPosition;

        [SerializeField]
        private float delayPoolSeconds;

        private PrefabPool<AttackDeclarationElement> pool;

        void Awake()
        {
            this.pool = new PrefabPool<AttackDeclarationElement>(this.elementPrefab);
        }

        private void OnDestroy()
        {
            this.pool.Clear();
            this.pool = null;
        }

        public async void Create(string message, ActorType actorType)
        {
            var element = this.pool.Get();
            element.transform.SetParent(this.transform, false);
            element.Setup(message);
            var position = actorType == ActorType.Player
                ? this.playerPosition.localPosition
                : this.enemyPosition.localPosition;
            element.SetPosition(position);

            await UniTask.Delay(TimeSpan.FromSeconds(this.delayPoolSeconds));

            this.pool?.Release(element);
        }
    }
}
