using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Localization;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class FieldData
    {
        public int id;

        public LocalizedString nameKey;

        public string Name => this.nameKey.GetLocalizedString();
    }

    [Serializable]
    public sealed class FieldSpec
    {
        public int id;

        public string nameKey;

        [Serializable]
        public sealed class Json
        {
            public List<FieldSpec> elements;
        }
    }
}
