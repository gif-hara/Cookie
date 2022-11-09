using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CriticalEffectUIView : UIView
    {
        [SerializeField]
        private AnimationController animationController;

        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private AnimationClip animationClip;

        private void Awake()
        {
            this.canvasGroup.alpha = 0.0f;
        }
        
        public void Play()
        {
            this.animationController.Play(this.animationClip);
        }
    }
}
