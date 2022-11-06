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
    public sealed class AttackDeclarationElement : MonoBehaviour
    {
        [SerializeField]
        private RectTransform root;
        
        [SerializeField]
        private TextMeshProUGUI value;
        
        public void SetPosition(Vector2 position)
        {
            this.root.localPosition = position;
        }
        
        public void Setup(string message)
        {
            this.value.SetText(message);
        }
    }
}
