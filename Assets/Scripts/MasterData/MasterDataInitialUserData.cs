using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataInitialUserData")]
    public sealed class MasterDataInitialUserData : MasterData<MasterDataInitialUserData>
    {
        public Weapon weapon;

        public Armor armor;

        public Accessory accessory;

        public List<int> unlockEnemies;

        public List<int> unlockWeapons;

        public List<int> unlockArmors;

        public List<int> unlockAccessories;
    }
}
