using System.Collections.Generic;
using UnityEngine;

namespace Cookie
{
    /// <summary>
    /// 矩形内のランダムに座標を設定するクラス
    /// </summary>
    public sealed class SetPositionRandomRect : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> targets;

        [SerializeField]
        private Rect rect;

        private void OnEnable()
        {
            foreach (var target in this.targets)
            {
                var position = new Vector3(
                    Random.Range(this.rect.xMin, this.rect.xMax),
                    Random.Range(this.rect.yMin, this.rect.yMax),
                    0.0f
                    );

                target.localPosition = position;
            }
        }
    }
}
