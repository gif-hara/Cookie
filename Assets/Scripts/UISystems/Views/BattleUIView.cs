using System.Collections.Generic;
using UnityEngine;

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

        public ActorStatusView PlayerStatusView => this.playerStatusView;

        public ActorStatusView EnemyStatusView => this.enemyStatusView;
    }
}
