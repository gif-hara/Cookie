using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class PassiveSkill : IIdHolder
    {
        public int id;
        
        public LocalizedString name;
        
        public PassiveSkillType skillType;
        
        public List<Attribute> attributes = new();

        public int Id => this.id;

        public string Name => this.name.GetLocalizedString();
    }

    [Serializable]
    public sealed class PassiveSkillWithWeight : WithWeight<PassiveSkill>
    {
    }
}
