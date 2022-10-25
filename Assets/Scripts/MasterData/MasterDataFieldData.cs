using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Cookie/MasterData/MasterDataFieldData")]
    public sealed class MasterDataFieldData : MasterData<MasterDataFieldData>
    {
        public List<FieldData> records;
    }
}
