using TMPro;
using UnityEngine;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NotifyUIView : UIView
    {
        [SerializeField]
        private CookieButton button;

        [SerializeField]
        private TextMeshProUGUI message;

        public CookieButton Button => this.button;

        public TextMeshProUGUI Message => this.message;
    }
}
