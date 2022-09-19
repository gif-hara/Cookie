using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataGacha")]
    public sealed class MasterDataWeaponGacha : MasterData<MasterDataWeaponGacha>
    {
        public List<WeaponGacha> gachas;
    }
    
    
}
