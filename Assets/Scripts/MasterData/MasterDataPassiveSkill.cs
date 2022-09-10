using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataPassiveSkill")]
    public sealed class MasterDataPassiveSkill : MasterData<MasterDataPassiveSkill>
    {
        public List<PassiveSkill> skills;
    }
}
