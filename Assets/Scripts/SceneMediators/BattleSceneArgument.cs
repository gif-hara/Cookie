using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleSceneArgument : ISceneArgument
    {
        public ActorStatusBuilder playerStatusBuilder;

        public ActorStatusBuilder enemyStatusBuilder;

        /// <summary>
        /// バトルが終了した際の拡張処理
        /// </summary>
        public Action<BattleJudgement> onBattleEnd;

        /// <summary>
        /// バトル処理が開放された際の拡張処理
        /// </summary>
        public Action onBattleFinalize;
    }
}
