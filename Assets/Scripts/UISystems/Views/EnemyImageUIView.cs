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
        private AnimationClip appearanceClip;

        [SerializeField]
        private AnimationClip damageClip;

        public async UniTask SetupAsync(int enemySpriteId)
        {
            this.image.enabled = false;
            var enemySprite = await AssetLoader.LoadAsync<Sprite>($"Assets/Textures/Enemy/{enemySpriteId}.jpg");
            this.image.sprite = enemySprite;
        }

        public async UniTask PlayAppearanceAsync()
        {
            this.image.enabled = true;
            await this.animationController.PlayTask(this.appearanceClip);
        }

        public async UniTask PlayDamageAsync()
        {
            await this.animationController.PlayTask(this.damageClip);
        }
    }
}
