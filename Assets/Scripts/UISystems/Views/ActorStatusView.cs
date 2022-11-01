using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
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

        public Slider HitPointSlider => this.hitPointSlider;

        public TextMeshProUGUI ActorName => this.actorName;

        public async UniTask PlayDamageAsync()
        {
            await this.animationController.PlayTask(this.damageClip);
        }
    }
}
