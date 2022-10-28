using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NotifyUIView : UIView
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI message;

        public Button Button => this.button;

        public TextMeshProUGUI Message => this.message;

        public void Show()
        {
            UIManager.Show(this);
        }

        public void Hidden()
        {
            UIManager.Hidden(this);
        }

        public void SetAsLastSibling()
        {
            UIManager.SetAsLastSibling(this);
        }
    }
}
