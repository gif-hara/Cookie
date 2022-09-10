using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataActiveSkill")]
    public sealed class MasterDataActiveSkill : MasterData<MasterDataActiveSkill>
    {
        public List<ActiveSkill> skills;
    }
}
