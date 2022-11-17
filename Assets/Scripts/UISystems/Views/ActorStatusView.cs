using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorStatusView : UIView
    {
        [SerializeField]
        private Slider hitPointSlider;

        [SerializeField]
        private TextMeshProUGUI actorName;

        [SerializeField]
        private AnimationController animationController;

        [SerializeField]
        private AnimationClip damageClip;

        [SerializeField]
        private AbnormalStatusIconElement abnormalStatusIconElementPrefab;

        [SerializeField]
        private RectTransform abnormalStatusIconParent;

        [SerializeField]
        private PoolablePrefab paralysisEffectPrefab;

        [SerializeField]
        private RectTransform paralysisEffectParent;

        private Dictionary<AbnormalStatus, AbnormalStatusIconElement> abnormalStatusIconDictionary = new();

        private PrefabPool<PoolablePrefab> paralysisEffectPool;

        public Slider HitPointSlider => this.hitPointSlider;

        public TextMeshProUGUI ActorName => this.actorName;

        public async UniTask PlayDamageAsync()
        {
            await this.animationController.PlayTask(this.damageClip);
        }

        public void AddAbnormalStatusIcon(AbnormalStatus abnormalStatus, Sprite icon)
        {
            Assert.IsFalse(this.abnormalStatusIconDictionary.ContainsKey(abnormalStatus));
            var element = Instantiate(this.abnormalStatusIconElementPrefab, this.abnormalStatusIconParent);
            element.Icon.sprite = icon;
            this.abnormalStatusIconDictionary[abnormalStatus] = element;
        }

        public void RemoveAbnormalStatusIcon(AbnormalStatus abnormalStatus)
        {
            Assert.IsTrue(this.abnormalStatusIconDictionary.ContainsKey(abnormalStatus));
            var element = this.abnormalStatusIconDictionary[abnormalStatus];
            this.abnormalStatusIconDictionary.Remove(abnormalStatus);
            Destroy(element.gameObject);
        }

        public async UniTask PlayParalysisEffect()
        {
            this.paralysisEffectPool ??= new PrefabPool<PoolablePrefab>(this.paralysisEffectPrefab);
            var effect = this.paralysisEffectPool.Get();
            effect.transform.SetParent(this.paralysisEffectParent, false);
            await UniTask.WhenAll(
                this.PlayDamageAsync(),
                UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: this.GetCancellationTokenOnDestroy())
                );
            this.paralysisEffectPool.Release(effect);
        }
    }
}
