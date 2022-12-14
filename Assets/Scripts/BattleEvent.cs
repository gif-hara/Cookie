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

        /// <summary>
        /// <see cref="Actor"/>がダメージを受けた際のメッセージ
        /// </summary>
        public class TakedDamage : Message<TakedDamage, Actor, DamageData>
        {
            public Actor Actor => this.Param1;
            
            public DamageData DamageData => this.Param2;
        }
        
        /// <summary>
        /// <see cref="Actor"/>が状態異常を付与された際のメッセージ
        /// </summary>
        public class AddedAbnormalStatus : Message<AddedAbnormalStatus, Actor, AbnormalStatus>
        {
            public Actor Actor => this.Param1;

            public AbnormalStatus AbnormalStatus => this.Param2;
        }

        /// <summary>
        /// <see cref="Actor"/>が状態異常を削除された際のメッセージ
        /// </summary>
        public class RemovedAbnormalStatus : Message<RemovedAbnormalStatus, Actor, AbnormalStatus>
        {
            public Actor Actor => this.Param1;

            public AbnormalStatus AbnormalStatus => this.Param2;
        }

        /// <summary>
        /// 攻撃宣言を行うメッセージ
        /// </summary>
        public class AttackDeclaration : Message<AttackDeclaration, Actor, string>
        {
            public Actor Actor => this.Param1;

            public string Message => this.Param2;
        }

        /// <summary>
        /// ダメージを与えた際のメッセージ
        /// </summary>
        public class GivedDamage : Message<GivedDamage, Actor, Actor, DamageData, ActiveSkill>
        {
            public Actor Attacker => this.Param1;

            public Actor Target => this.Param2;

            public DamageData DamageData => this.Param3;

            public ActiveSkill ActiveSkill => this.Param4;
        }

        /// <summary>
        /// 回復した際のメッセージ
        /// </summary>
        public class Recovered : Message<Recovered, Actor, int>
        {
            public Actor Actor => this.Param1;

            public int Value => this.Param2;
        }
        
        /// <summary>
        /// 麻痺が発動した際のメッセージ
        /// </summary>
        public class InvokedParalysis : Message<InvokedParalysis, Actor>
        {
            public Actor Actor => this.Param1;
        }
        
        /// <summary>
        /// 毒が発動した際のメッセージ
        /// </summary>
        public class InvokedPoison : Message<InvokedPoison, Actor>
        {
            public Actor Actor => this.Param1;
        }
    }
}
