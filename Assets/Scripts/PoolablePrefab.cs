using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// Poolされる想定のプレハブにアタッチされるクラス
    /// </summary>
    public sealed class PoolablePrefab : MonoBehaviour
    {
        private float particleTotalTime = -1.0f;
        
        public float GetParticleTotalSeconds()
        {
            if (this.particleTotalTime <= -1.0f)
            {
                this.particleTotalTime = this.GetComponentsInChildren<ParticleSystem>().Max(x => x.totalTime);
            }

            return this.particleTotalTime;
        }
    }
}
