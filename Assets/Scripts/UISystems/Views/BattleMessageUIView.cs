using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleMessageUIView : UIView
    {
        public enum Type
        {
            BattleStart,
            Win,
            Lose,
        }

        [SerializeField]
        private List<TypeData> typeDataList;

        [SerializeField]
        private AnimationController animationController;

        private void Awake()
        {
            foreach (var typeData in this.typeDataList)
            {
                typeData.root.SetActive(false);
            }
        }

        public async UniTask Play(Type type)
        {
            foreach (var typeData in this.typeDataList)
            {
                var isTarget = typeData.type == type;
                typeData.root.SetActive(isTarget);
                if (isTarget)
                {
                    await this.animationController.PlayTask(typeData.clip);
                }
            }
        }

        [Serializable]
        public class TypeData
        {
            public Type type;

            public GameObject root;

            public AnimationClip clip;
        }
    }
}
