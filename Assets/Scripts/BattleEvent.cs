using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// バトルで利用するイベント
    /// </summary>
    public sealed class BattleEvent
    {
        /// <summary>
        /// バトルが開始した際のメッセージ
        /// </summary>
        public class StartBattle : Message<StartBattle>
        {
        }

        /// <summary>
        /// バトルが開放された際のメッセージ
        /// </summary>
        public class Dispose : Message<Dispose>
        {
        }

        /// <summary>
        /// ターンが開始された際のメッセージ
        /// </summary>
        public class StartTurn : Message<StartTurn, Actor>
        {
            /// <summary>
            /// 対戦相手
            /// </summary>
            public Actor Opponent => this.Param1;
        }
    }
}
