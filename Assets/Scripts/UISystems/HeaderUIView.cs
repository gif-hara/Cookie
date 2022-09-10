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

        [SerializeField]
        private CookieButton gachaButton;

        [SerializeField]
        private CookieButton editEquipmentButton;

        [SerializeField]
        private CookieButton selectEnemyButton;

        [SerializeField]
        private CookieButton optionButton;

        [SerializeField]
        private GameObject menuRoot;

        public CookieButton RootButton => this.rootButton;

        public CookieButton GachaButton => this.gachaButton;

        public CookieButton EditEquipmentButton => this.editEquipmentButton;

        public CookieButton SelectEnemyButton => this.selectEnemyButton;

        public GameObject MenuRoot => this.menuRoot;

        private void Awake()
        {
            this.menuRoot.SetActive(false);
        }
        
    }
}
