using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UIStylist : MonoBehaviour
    {
        [SerializeReference, SubclassSelector(typeof(IUIStyleAction))]
        private List<IUIStyleAction> styleActions;

        [ContextMenu("Apply")]
        public void Apply()
        {
            foreach (var styleAction in this.styleActions)
            {
                styleAction.Apply();
            }
        }
    }

    [Serializable]
    public sealed class ComparisonUIStylists
    {
        [SerializeField]
        private UIStylist positive;

        [SerializeField]
        private UIStylist negative;
        
        [SerializeField]
        private UIStylist equal;

        public void Apply(int value)
        {
            switch (value)
            {
                case > 0:
                    this.positive.Apply();
                    break;
                case < 0:
                    this.negative.Apply();
                    break;
                default:
                    this.equal.Apply();
                    break;
            }
        }
    }
}
