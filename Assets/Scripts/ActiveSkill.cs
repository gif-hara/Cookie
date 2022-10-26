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

        public int attachMax;
        
        public int Id => this.id;

        public string Name => this.name.GetLocalizedString();
    }

    [Serializable]
    public sealed class ActiveSkillWithWeight : WithWeight<ActiveSkill>
    {
    }

    [Serializable]
    public sealed class ActiveSkillSpec
    {
        public int id;

        public string nameKey;

        public int attachMax;
        
        [Serializable]
        public class Json
        {
            public List<ActiveSkillSpec> elements;
        }
    }

    [Serializable]
    public sealed class ActiveSkillAttribute
    {
        public int id;

        public int activeSkillId;

        public string key;

        public int value;
        
        [Serializable]
        public class Json
        {
            public List<ActiveSkillAttribute> elements;
        }
    }
}
