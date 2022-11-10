using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleHeaderUIView : MonoBehaviour
    {
        [SerializeField]
        private CookieButton escapeButton;

        [SerializeField]
        private CookieButton speedButton;

        public CookieButton EscapeButton => this.escapeButton;

        public CookieButton SpeedButton => this.speedButton;

        public void SetSpeedButtonMessage(BattleSpeedType battleSpeedType)
        {
            this.speedButton.Message.text = battleSpeedType.GetMessage();
        }
    }
}
