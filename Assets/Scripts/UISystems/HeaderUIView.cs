using System;
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

        public CookieButton RootButton => this.rootButton;
    }
}
