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
        private RectTransform activeSkillRoot;

        [SerializeField]
        private ActiveSkillUIElement activeSkillUIElementPrefab;

        [SerializeField]
        private ComparisonUIStylists physicalStrengthComparisonUIStylists;

        [SerializeField]
        private ComparisonUIStylists magicStrengthComparisonUIStylists;

        [SerializeField]
        private ComparisonUIStylists totalStrengthComparisonUIStylists;
        
        private readonly List<ActiveSkillUIElement> activeSkillUIElements = new();

        public TextMeshProUGUI WeaponName => this.weaponName;

        public TextMeshProUGUI PhysicalStrength => this.physicalStrength;
        
        public TextMeshProUGUI MagicStrength => this.magicStrength;

        public TextMeshProUGUI TotalStrength => this.totalStrength;

        public ComparisonUIStylists PhysicalStrengthComparisonUIStylists => this.physicalStrengthComparisonUIStylists;

        public ComparisonUIStylists MagicStrengthComparisonUIStylists => this.magicStrengthComparisonUIStylists;

        public ComparisonUIStylists TotalStrengthComparisonUIStylists => this.totalStrengthComparisonUIStylists;

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
    }
}
