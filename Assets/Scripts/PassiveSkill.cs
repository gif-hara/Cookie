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
        
        public List<Attribute> attributes = new();

        public int Id => this.id;

        public string Name => this.name.GetLocalizedString();
    }

    [Serializable]
    public sealed class PassiveSkillWithWeight : WithWeight<PassiveSkill>
    {
    }
    
    [Serializable]
    public sealed class PassiveSkillSpec
    {
        public int id;

        public string nameKey;
        
        [Serializable]
        public class Json
        {
            public List<PassiveSkillSpec> elements;
        }
    }
    
    [Serializable]
    public sealed class PassiveSkillAttribute
    {
        public int id;

        public int passiveSkillId;

        public string key;

        public int value;
        
        [Serializable]
        public class Json
        {
            public List<PassiveSkillAttribute> elements;
        }
    }
}
