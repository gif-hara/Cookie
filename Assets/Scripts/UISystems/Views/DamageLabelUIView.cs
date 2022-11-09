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
        private DamageLabelElement damageLabelPrefab;
        
        [SerializeField]
        private DamageLabelElement recoveryLabelPrefab;
        
        [SerializeField]
        private RectTransform playerPosition;

        [SerializeField]
        private RectTransform enemyPosition;

        [SerializeField]
        private float delayPoolSeconds;

        private readonly PrefabPoolDictionary<DamageLabelElement> poolDictionary = new();
        
        private void OnDestroy()
        {
            this.poolDictionary.ClearAll();
        }

        public async UniTask CreateDamageLabel(int damage, ActorType actorType)
        {
            await this.CreateLabel(damage, actorType, this.poolDictionary.Get(this.damageLabelPrefab));
        }

        public async UniTask CreateRecoveryLabel(int value, ActorType actorType)
        {
            await this.CreateLabel(value, actorType, this.poolDictionary.Get(this.recoveryLabelPrefab));
        }

        private async UniTask CreateLabel(int value, ActorType actorType, PrefabPool<DamageLabelElement> pool)
        {
            var element = pool.Get();
            element.transform.SetParent(this.transform, false);
            element.Setup(value);
            var position = actorType == ActorType.Player
                ? this.playerPosition.localPosition
                : this.enemyPosition.localPosition;
            element.SetPosition(position);

            await UniTask.Delay(TimeSpan.FromSeconds(this.delayPoolSeconds), cancellationToken:this.GetCancellationTokenOnDestroy());

            pool?.Release(element);
        }
    }
}
