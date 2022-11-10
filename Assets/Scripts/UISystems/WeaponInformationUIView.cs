using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WeaponInformationUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI weaponName;

        [SerializeField]
        private TextMeshProUGUI physicalStrength;

        [SerializeField]
        private TextMeshProUGUI magicStrength;

        [SerializeField]
        private TextMeshProUGUI totalStrength;

        [SerializeField]
        private TextMeshProUGUI criticalRate;

        [SerializeField]
        private RectTransform activeSkillRoot;

        [SerializeField]
        private ActiveSkillUIElement activeSkillUIElementPrefab;

        [SerializeField]
        private ComparisonUIStylists physicalStrengthComparisonUIStylists;

        [SerializeField]
        private ComparisonUIStylists magicStrengthComparisonUIStylists;

        [SerializeField]
        private ComparisonUIStylists totalStrengthComparisonUIStylists;
        
        [SerializeField]
        private ComparisonUIStylists criticalRateComparisonUIStylists;

        [SerializeField]
        private RareEffectHolder rareEffectHolder;

        [SerializeField]
        private RectTransform physicalStrengthEffectParent;

        [SerializeField]
        private RectTransform magicStrengthEffectParent;

        [SerializeField]
        private RectTransform criticalRateEffectParent;

        private readonly List<ActiveSkillUIElement> activeSkillUIElements = new();

        private readonly List<GameObject> rareEffects = new();

        public TextMeshProUGUI WeaponName => this.weaponName;

        public TextMeshProUGUI PhysicalStrength => this.physicalStrength;
        
        public TextMeshProUGUI MagicStrength => this.magicStrength;

        public TextMeshProUGUI TotalStrength => this.totalStrength;

        public TextMeshProUGUI CriticalRate => this.criticalRate;

        public ComparisonUIStylists PhysicalStrengthComparisonUIStylists => this.physicalStrengthComparisonUIStylists;

        public ComparisonUIStylists MagicStrengthComparisonUIStylists => this.magicStrengthComparisonUIStylists;

        public ComparisonUIStylists TotalStrengthComparisonUIStylists => this.totalStrengthComparisonUIStylists;

        public ComparisonUIStylists CriticalRateComparisonUIStylists => this.criticalRateComparisonUIStylists;

        public RectTransform PhysicalStrengthEffectParent => this.physicalStrengthEffectParent;

        public RectTransform MagicStrengthEffectParent => this.magicStrengthEffectParent;

        public RectTransform CriticalRateEffectParent => this.criticalRateEffectParent;

        public void DestroyAllActiveSkillUIElements()
        {
            foreach (var activeSkillUIElement in this.activeSkillUIElements)
            {
                Destroy(activeSkillUIElement.gameObject);
            }
            
            this.activeSkillUIElements.Clear();
        }

        public ActiveSkillUIElement CreateActiveSkillUIElement()
        {
            var result = Instantiate(this.activeSkillUIElementPrefab, this.activeSkillRoot);
            this.activeSkillUIElements.Add(result);

            return result;
        }

        public void DestroyAllRareEffects()
        {
            foreach (var rareEffect in this.rareEffects)
            {
                Destroy(rareEffect);
            }
            
            this.rareEffects.Clear();
        }

        public GameObject CreateRareEffect(Rare rare)
        {
            var result = this.rareEffectHolder.Create(rare);
            this.rareEffects.Add(result);

            return result;
        }
    }
}
