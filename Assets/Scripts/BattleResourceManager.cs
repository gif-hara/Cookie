using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MessagePipe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleResourceManager : IDisposable
    {
        public readonly Dictionary<AbnormalStatus, Sprite> AbnormalStatusIcons = new();
        
        public async UniTask SetupAsync(DisposableBagBuilder scope)
        {
            var abnormalStatuses = new[]
            {
                AbnormalStatus.Poison,
                AbnormalStatus.Paralysis,
                AbnormalStatus.Debility,
                AbnormalStatus.Fragility,
            };
            var abnormalStatsIcons = await UniTask.WhenAll(
                abnormalStatuses.Select(x => AssetLoader.LoadAsync<Sprite>($"Assets/Textures/UI/AbnormalStatus{(int)x}.png"))
                );
            for (var i = 0; i < abnormalStatuses.Length; i++)
            {
                this.AbnormalStatusIcons.Add(abnormalStatuses[i], abnormalStatsIcons[i]);
            }
            
            scope.Add(this);
        }
        
        public void Dispose()
        {
            this.AbnormalStatusIcons.Clear();
        }
    }
}
