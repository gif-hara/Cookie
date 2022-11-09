using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleEffectUIView : UIView
    {
        [SerializeField]
        private List<PoolablePrefab> effectPrefabs;

        [SerializeField]
        private Transform playerPosition;

        [SerializeField]
        private Transform enemyPosition;

        [SerializeField]
        private float delayPoolSeconds;
        
        private PrefabPoolDictionary<PoolablePrefab> pool;

        void Awake()
        {
            this.pool = new PrefabPoolDictionary<PoolablePrefab>();
        }

        private void OnDestroy()
        {
            this.pool.ClearAll();
            this.pool = null;
        }

        public async UniTask Create(int index, ActorType actorType)
        {
            var pool = this.pool.Get(this.effectPrefabs[index]);
            var element = pool.Get();
            element.transform.SetParent(this.transform, false);
            var position = actorType == ActorType.Player
                ? this.playerPosition.localPosition
                : this.enemyPosition.localPosition;
            element.transform.localPosition = position;

            await UniTask.Delay(TimeSpan.FromSeconds(this.delayPoolSeconds));

            pool.Release(element);
        }
    }
}
