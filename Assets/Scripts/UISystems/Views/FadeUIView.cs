using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Cookie.UISystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FadeUIView : UIView
    {
        public enum FadeType
        {
            Basic
        }

        [Serializable]
        public class FadeData
        {
            public FadeType fadeType;

            public AnimationController animationController;

            public AnimationClip inClip;

            public AnimationClip outClip;
        }

        [SerializeField]
        private List<FadeData> fadeDataList;

        public async UniTask PlayAsync(FadeType fadeType, UniTask awaiter)
        {
            var fadeData = this.fadeDataList.Find(x => x.fadeType == fadeType);
            Assert.IsNotNull(fadeData, $"{fadeType}");

            await fadeData.animationController.PlayTask(fadeData.inClip);
            await awaiter;
            await fadeData.animationController.PlayTask(fadeData.outClip);
        }
    }
}
