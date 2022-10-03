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
    }
}
