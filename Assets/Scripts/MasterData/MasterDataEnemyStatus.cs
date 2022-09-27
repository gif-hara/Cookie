using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataEnemyStatus")]
    public sealed class MasterDataEnemyStatus : MasterData<MasterDataEnemyStatus>
    {
        public List<EnemyStatus> enemyStatusList;
    }
}
