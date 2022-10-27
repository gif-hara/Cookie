using System;
using Cookie.UISystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StartMenuUIView : UIView
    {
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

        public CookieButton GachaButton => this.gachaButton;

        public CookieButton EditEquipmentButton => this.editEquipmentButton;

        public CookieButton SelectEnemyButton => this.selectEnemyButton;

        public GameObject MenuRoot => this.menuRoot;
    }
}
