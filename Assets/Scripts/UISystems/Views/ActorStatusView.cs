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

        public Slider HitPointSlider => this.hitPointSlider;

        public TextMeshProUGUI ActorName => this.actorName;
    }
}
