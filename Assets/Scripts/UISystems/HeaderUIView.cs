using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HeaderUIView : MonoBehaviour
    {
        [SerializeField]
        private CookieButton rootButton;

        [SerializeField]
        private TextMeshProUGUI money;

        public CookieButton RootButton => this.rootButton;

        public TextMeshProUGUI Money => this.money;
    }
}
