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
        
        void Awake()
        {
            this.image.enabled = false;
        }

        public async UniTask SetupAsync(int enemySpriteId)
        {
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
    }
}
