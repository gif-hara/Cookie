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

        [SerializeField]
        private RareEffectHolder rareEffectHolder;

        [SerializeField]
        private RectTransform rareEffectParent;

        public TextMeshProUGUI Index => this.index;

        public TextMeshProUGUI NameText => this.nameText;

        public void CreateRareEffect(Rare rare)
        {
            var effect = this.rareEffectHolder.Create(rare);
            effect.transform.SetParent(this.rareEffectParent, false);
        }
    }
}
