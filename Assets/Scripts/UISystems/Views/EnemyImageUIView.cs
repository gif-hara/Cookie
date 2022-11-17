using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EnemyImageUIView : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private AnimationController animationController;

        [SerializeField]
        private List<AnimationClip> appearanceClips;

        [SerializeField]
        private AnimationClip damageClip;

        [SerializeField]
        private List<AnimationClip> diedClips;

        [SerializeField]
        private PoolablePrefab paralysisEffectPrefab;

        [SerializeField]
        private RectTransform paralysisEffectParent;

        private PrefabPool<PoolablePrefab> paralysisEffectPool;

        public async UniTask SetupAsync(int enemySpriteId)
        {
            this.paralysisEffectPool = new PrefabPool<PoolablePrefab>(this.paralysisEffectPrefab);
            
            this.image.enabled = false;
            var enemySprite = await AssetLoader.LoadAsync<Sprite>($"Assets/Textures/Enemy/{enemySpriteId}.jpg");
            this.image.sprite = enemySprite;
        }

        public async UniTask PlayAppearanceAsync(int clipId)
        {
            this.image.enabled = true;
            await this.animationController.PlayTask(this.appearanceClips[clipId]);
        }

        public async UniTask PlayDamageAsync()
        {
            await this.animationController.PlayTask(this.damageClip);
        }

        public async UniTask PlayDiedAsync(int animationId)
        {
            await this.animationController.PlayTask(this.diedClips[animationId]);
        }

        public UniTask WaitForAnimation()
        {
            return this.animationController.WaitForAnimation();
        }

        public async UniTask PlayParalysisEffect()
        {
            var effect = this.paralysisEffectPool.Get();
            effect.transform.SetParent(this.paralysisEffectParent, false);
            await UniTask.WhenAll(
                this.PlayDamageAsync(),
                UniTask.Delay(TimeSpan.FromSeconds(1.5f))
                );
            this.paralysisEffectPool.Release(effect);
        }
    }
}
