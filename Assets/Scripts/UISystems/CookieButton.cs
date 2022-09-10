using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CookieButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI message;

        [SerializeField]
        private UIStylist positiveStylist;

        [SerializeField]
        private UIStylist selectedStylist;

        public Button Button => this.button;

        public TextMeshProUGUI Message => this.message;

        public UIStylist PositiveStylist => this.positiveStylist;

        public UIStylist SelectedStylist => this.selectedStylist;

        public UnityEvent OnPointerEnter
        {
            get;
        } = new();

        void Awake()
        {
            this.positiveStylist.Apply();
        }
        
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            this.OnPointerEnter.Invoke();
        }
    }
}
