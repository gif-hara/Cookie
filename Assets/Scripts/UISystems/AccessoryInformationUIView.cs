using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AccessoryInformationUIView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI accessoryName;
        
        [SerializeField]
        private RectTransform passiveSkillRoot;

        [SerializeField]
        private PassiveSkillUIElement passiveSkillUIElementPrefab;
        
        private readonly List<PassiveSkillUIElement> passiveSkillUIElements = new();

        public TextMeshProUGUI AccessoryName => this.accessoryName;
        
        public void DestroyAllPassiveSkillUIElements()
        {
            foreach (var activeSkillUIElement in this.passiveSkillUIElements)
            {
                Destroy(activeSkillUIElement.gameObject);
            }
            
            this.passiveSkillUIElements.Clear();
        }

        public PassiveSkillUIElement CreatePassiveSkillUIElement()
        {
            var result = Instantiate(this.passiveSkillUIElementPrefab, this.passiveSkillRoot);
            this.passiveSkillUIElements.Add(result);

            return result;
        }
    }
}
