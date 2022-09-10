using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class PassiveSkill
    {
        public int id;
        
        public LocalizedString name;
        
        public PassiveSkillType skillType;
        
        public List<Attribute> attributes = new();
    }

    [Serializable]
    public sealed class PassiveSkillWithWeight : WithWeight<PassiveSkill>
    {
    }
}
