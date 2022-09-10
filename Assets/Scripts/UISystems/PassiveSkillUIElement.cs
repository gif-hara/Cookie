using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PassiveSkillUIElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI index;

        [SerializeField]
        private TextMeshProUGUI nameText;

        public TextMeshProUGUI Index => this.index;

        public TextMeshProUGUI NameText => this.nameText;
    }
}
