using System;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Actor
    {
        private readonly ActorType actorType;

        public readonly ActorStatus Status;

        public Actor(ActorType actorType, ActorStatus status)
        {
            this.actorType = actorType;
            this.Status = status;

            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<BattleEvent.StartBattle>()
                .Subscribe(_ =>
                {
                    Debug.Log($"StartBattle {this.actorType}");
                })
                .AddTo(bag);

            GlobalMessagePipe.GetSubscriber<BattleEvent.Dispose>()
                .Subscribe(_ =>
                {
                    bag.Build().Dispose();
                })
                .AddTo(bag);
            
            GlobalMessagePipe.GetAsyncSubscriber<Actor, BattleEvent.StartTurn>()
                .Subscribe(this, async (x, cancelToken) =>
                {
                    Debug.Log($"StartTurn {this.actorType}");
                    foreach (var skill in this.Status.activeSkills)
                    {
                        var skillType = (ActiveSkillType)skill.attributes.Get(SkillAttributeName.ActiveSkillType).value;
                        switch (skillType)
                        {
                            case ActiveSkillType.Attack:
                                x.Opponent.TakeDamage(Calculator.GetDamage(this.Status, skill, x.Opponent.Status));
                                break;
                            case ActiveSkillType.Recovery:
                            default:
                                Assert.IsTrue(false, $"{skillType}は未対応です");
                                break;
                        }
                        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
                        
                        // 相手が死亡していたら強制的に終了する
                        if (x.Opponent.Status.IsDead)
                        {
                            break;
                        }
                    }
                })
                .AddTo(bag);
        }

        private void TakeDamage(int damage)
        {
            this.Status.hitPoint -= damage;
            Debug.Log($"TakeDamage {this.actorType} hitPoint = {this.Status.hitPoint}");
        }
    }
}
