using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ArmorInformationUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI armorName;

        [SerializeField]
        private TextMeshProUGUI physicalDefense;

        [SerializeField]
        private TextMeshProUGUI magicDefense;

        [SerializeField]
        private TextMeshProUGUI hitPoint;

        [SerializeField]
        private TextMeshProUGUI speed;
        
        [SerializeField]
        private ComparisonUIStylists physicalDefenseComparisonUIStylists;

        [SerializeField]
        private ComparisonUIStylists magicDefenseComparisonUIStylists;

        [SerializeField]
        private ComparisonUIStylists hitPointComparisonUIStylists;

        [SerializeField]
        private ComparisonUIStylists speedComparisonUIStylists;

        [SerializeField]
        private RareEffectHolder rareEffectHolder;

        [SerializeField]
        private RectTransform physicalDefenseEffectParent;

        [SerializeField]
        private RectTransform magicDefenseEffectParent;

        [SerializeField]
        private RectTransform hitPointEffectParent;

        [SerializeField]
        private RectTransform speedEffectParent;

        private readonly List<GameObject> rareEffects = new();
        
        public TextMeshProUGUI ArmorName => this.armorName;

        public TextMeshProUGUI PhysicalDefense => this.physicalDefense;
        
        public TextMeshProUGUI MagicDefense => this.magicDefense;
        
        public TextMeshProUGUI HitPoint => this.hitPoint;
        
        public TextMeshProUGUI Speed => this.speed;

        public ComparisonUIStylists PhysicalDefenseComparisonUIStylists => this.physicalDefenseComparisonUIStylists;

        public ComparisonUIStylists MagicDefenseComparisonUIStylists => this.magicDefenseComparisonUIStylists;

        public ComparisonUIStylists HitPointComparisonUIStylists => this.hitPointComparisonUIStylists;

        public ComparisonUIStylists SpeedComparisonUIStylists => this.speedComparisonUIStylists;

        public RectTransform PhysicalDefenseEffectParent => this.physicalDefenseEffectParent;

        public RectTransform MagicDefenseEffectParent => this.magicDefenseEffectParent;

        public RectTransform HitPointEffectParent => this.hitPointEffectParent;

        public RectTransform SpeedEffectParent => this.speedEffectParent;
        
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
