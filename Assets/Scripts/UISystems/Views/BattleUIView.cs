using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleUIView : UIView
    {
        [SerializeField]
        private ActorStatusView playerStatusView;

        [SerializeField]
        private ActorStatusView enemyStatusView;

        [SerializeField]
        private DamageLabelUIView damageLabelUIView;

        [SerializeField]
        private EnemyImageUIView enemyImageUIView;
        
        public ActorStatusView PlayerStatusView => this.playerStatusView;

        public ActorStatusView EnemyStatusView => this.enemyStatusView;

        public DamageLabelUIView DamageLabelUIView => this.damageLabelUIView;

        public EnemyImageUIView EnemyImageUIView => this.enemyImageUIView;
    }
}
