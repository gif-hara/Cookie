using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PopupUIView : UIView
    {
        [SerializeField]
        private CookieButton yesButton;

        [SerializeField]
        private CookieButton noButton;

        [SerializeField]
        private TextMeshProUGUI message;

        public CookieButton YesButton => this.yesButton;

        public CookieButton NoButton => this.noButton;

        public TextMeshProUGUI Message => this.message;
    }
}
