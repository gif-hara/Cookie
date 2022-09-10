using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ChangeGraphicColor : IUIStyleAction
    {
        [SerializeField]
        private Graphic image;

        [SerializeField]
        private Color color;
        
        public void Apply()
        {
            this.image.color = this.color;
        }
    }
}
