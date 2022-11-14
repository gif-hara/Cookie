using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cookie
{
    /// <summary>
    /// 対象のアクティブ状態をランダムな遅延時間で設定するクラス
    /// </summary>
    public sealed class SetActiveRandomDelay : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> targets;

        [SerializeField]
        private bool isActive;

        [SerializeField]
        private float delaySecondMin;

        [SerializeField]
        private float delaySecondMax;

        private void OnEnable()
        {
            foreach (var target in this.targets)
            {
                var delaySeconds = Random.Range(this.delaySecondMin, this.delaySecondMax);
                SetActiveAsync(target, this.isActive, delaySeconds).Forget();
            }
        }

        private static async UniTask SetActiveAsync(GameObject target, bool isActive, float delaySeconds)
        {
            target.SetActive(!isActive);

            await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken: target.GetCancellationTokenOnDestroy());
            
            target.SetActive(isActive);
        }
    }
}
