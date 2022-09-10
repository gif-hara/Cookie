using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataAccessoryGacha")]
    public sealed class MasterDataAccessoryGacha : MasterData<MasterDataAccessoryGacha>
    {
        public List<AccessoryGacha> gachas;
    }
}
