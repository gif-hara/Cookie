using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AbnormalStatusIconElement : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        public Image Icon => this.icon;
    }
}
