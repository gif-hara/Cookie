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

            public float releaseDelaySeconds;
        }

        [Serializable]
        public class AbnormalStatusEffectData
        {
            public PoolablePrefab playerPrefab;

            public Transform playerPosition;

            public PoolablePrefab enemyPrefab;

            public Transform enemyPosition;

            public float releaseDelaySeconds;
        }

        [SerializeField]
        private AttackEffectData attackEffectData;

        [SerializeField]
        private AbnormalStatusEffectData paralysisEffectData;

        [SerializeField]
        private AbnormalStatusEffectData poisonEffectData;

        private readonly PrefabPoolDictionary<PoolablePrefab> poolDictionary = new ();
        
        private void OnDestroy()
        {
            this.poolDictionary.ClearAll();
        }

        /// <summary>
        /// 攻撃エフェクトを生成する
        /// </summary>
        public UniTask CreateAttackEffect(int index, ActorType actorType)
        {
            var point = actorType == ActorType.Player
                ? this.attackEffectData.playerPosition
                : this.attackEffectData.enemyPosition;
            return this.CreateInternal(this.attackEffectData.effectPrefabs[index], point, this.attackEffectData.releaseDelaySeconds);
        }

        /// <summary>
        /// 麻痺エフェクトを生成する
        /// </summary>
        public UniTask CreateParalysisEffect(ActorType actorType)
        {
            return this.CreateAbnormalStatusEffect(this.paralysisEffectData, actorType);
        }

        /// <summary>
        /// 毒エフェクトを生成する
        /// </summary>
        public UniTask CreatePoisonEffect(ActorType actorType)
        {
            return this.CreateAbnormalStatusEffect(this.poisonEffectData, actorType);
        }

        private UniTask CreateAbnormalStatusEffect(AbnormalStatusEffectData effectData, ActorType actorType)
        {
            var prefab = actorType == ActorType.Player
                ? effectData.playerPrefab
                : effectData.enemyPrefab;
            var point = actorType == ActorType.Player
                ? effectData.playerPosition
                : effectData.enemyPosition;

            return this.CreateInternal(prefab, point, effectData.releaseDelaySeconds);
        }

        private async UniTask CreateInternal(PoolablePrefab prefab, Transform point, float releaseDelaySeconds)
        {
            var pool = this.poolDictionary.Get(prefab);
            var element = pool.Get();
            element.transform.SetParent(this.transform, false);
            element.transform.localPosition = point.localPosition;

            await UniTask.Delay(TimeSpan.FromSeconds(releaseDelaySeconds), cancellationToken: this.GetCancellationTokenOnDestroy());

            pool.Release(element);
        }
    }
}
