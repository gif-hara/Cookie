using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DamageLabelElement : MonoBehaviour
    {
        [SerializeField]
        private RectTransform root;
        
        [SerializeField]
        private TextMeshProUGUI value;
        
        public void SetPosition(Vector2 position)
        {
            this.root.localPosition = position;
        }
        
        public void Setup(int damage)
        {
            this.value.SetText("{0}", damage);
        }
    }
}
