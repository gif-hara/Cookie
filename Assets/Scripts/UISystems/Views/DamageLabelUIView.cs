using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DamageLabelUIView : UIView
    {
        [SerializeField]
        private DamageLabelElement elementPrefab;
        
        [SerializeField]
        private RectTransform playerPosition;

        [SerializeField]
        private RectTransform enemyPosition;

        [SerializeField]
        private float delayPoolSeconds;

        private PrefabPool<DamageLabelElement> pool;

        void Awake()
        {
            this.pool = new PrefabPool<DamageLabelElement>(this.elementPrefab);
        }

        private void OnDestroy()
        {
            this.pool.Clear();
            this.pool = null;
        }

        public async void Create(int damage, ActorType actorType)
        {
            var element = this.pool.Get();
            element.transform.SetParent(this.transform, false);
            element.Setup(damage);
            var position = actorType == ActorType.Player
                ? this.playerPosition.localPosition
                : this.enemyPosition.localPosition;
            element.SetPosition(position);

            await UniTask.Delay(TimeSpan.FromSeconds(this.delayPoolSeconds));

            this.pool?.Release(element);
        }
    }
}
