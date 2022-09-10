using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataArmorGacha")]
    public sealed class MasterDataArmorGacha : MasterData<MasterDataArmorGacha>
    {
        public List<ArmorGacha> gachas;
    }
}
