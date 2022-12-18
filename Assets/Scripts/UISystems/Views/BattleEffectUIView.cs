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
        [Serializable]
        public class AttackEffectData
        {
            public List<PoolablePrefab> effectPrefabs;

            public Transform playerPosition;

            public Transform enemyPosition;
        }

        [SerializeField]
        private AttackEffectData attackEffectData;
        
        private PrefabPoolDictionary<PoolablePrefab> poolDictionary;

        void Awake()
        {
            this.poolDictionary = new PrefabPoolDictionary<PoolablePrefab>();
        }

        private void OnDestroy()
        {
            this.poolDictionary.ClearAll();
            this.poolDictionary = null;
        }

        /// <summary>
        /// 攻撃エフェクトを生成する
        /// </summary>
        public async UniTask CreateAttackEffect(int index, ActorType actorType)
        {
            var pool = this.poolDictionary.Get(this.attackEffectData.effectPrefabs[index]);
            var element = pool.Get();
            element.transform.SetParent(this.transform, false);
            var position = actorType == ActorType.Player
                ? this.attackEffectData.playerPosition.localPosition
                : this.attackEffectData.enemyPosition.localPosition;
            element.transform.localPosition = position;

            await UniTask.Delay(TimeSpan.FromSeconds(element.GetParticleTotalSeconds()), cancellationToken: this.GetCancellationTokenOnDestroy());

            pool.Release(element);
        }
    }
}
