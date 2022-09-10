using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataInitialEquipment")]
    public sealed class MasterDataInitialEquipment : MasterData<MasterDataInitialEquipment>
    {
        public Weapon weapon;

        public Armor armor;

        public Accessory accessory;
    }
}
