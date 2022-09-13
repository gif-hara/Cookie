using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataPlayerStatus")]
    public sealed class MasterDataPlayerStatus : MasterData<MasterDataPlayerStatus>
    {
        public List<PlayerStatus> playerStatusList;
    }
}
