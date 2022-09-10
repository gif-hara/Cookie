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
        
        public TextMeshProUGUI ArmorName => this.armorName;

        public TextMeshProUGUI PhysicalDefense => this.physicalDefense;
        
        public TextMeshProUGUI MagicDefense => this.magicDefense;
        
        public TextMeshProUGUI HitPoint => this.hitPoint;
        
        public TextMeshProUGUI Speed => this.speed;

        public ComparisonUIStylists PhysicalDefenseComparisonUIStylists => this.physicalDefenseComparisonUIStylists;

        public ComparisonUIStylists MagicDefenseComparisonUIStylists => this.magicDefenseComparisonUIStylists;

        public ComparisonUIStylists HitPointComparisonUIStylists => this.hitPointComparisonUIStylists;

        public ComparisonUIStylists SpeedComparisonUIStylists => this.speedComparisonUIStylists;
    }
}
