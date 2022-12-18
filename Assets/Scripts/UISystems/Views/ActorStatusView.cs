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
        
        private Dictionary<AbnormalStatus, AbnormalStatusIconElement> abnormalStatusIconDictionary = new();

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
    }
}
