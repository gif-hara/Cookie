using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GachaUIView : UIView
    {
        [SerializeField]
        private HeaderUIView headerUIView;

        public HeaderUIView HeaderUIView => this.headerUIView;
    }
}
