using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ActiveSkill : IIdHolder
    {
        public int id;
        
        public LocalizedString name;
        
        public List<Attribute> attributes = new();
        
        public int Id => this.id;

        public string Name => this.name.GetLocalizedString();
    }

    [Serializable]
    public sealed class ActiveSkillWithWeight : WithWeight<ActiveSkill>
    {
    }
}
