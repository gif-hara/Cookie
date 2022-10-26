using System;
using System.Linq;
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
                        // 麻痺の処理
                        if (this.Status.abnormalStatuses.Contains(AbnormalStatus.Paralysis) && Calculator.CanInvokeParalysis())
                        {
                            Debug.Log("TODO 麻痺演出");
                            await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
                            continue;
                        }
                        
                        // 実際の行動を処理する
                        foreach (var attribute in skill.attributes)
                        {
                            if (!attribute.name.StartsWith("Behaviour"))
                            {
                                continue;
                            }
                            switch (attribute.name)
                            {
                                case SkillAttributeName.BehaviourAttack:
                                    var damage = Calculator.GetDamage(this.Status, skill, x.Opponent.Status);
                                    x.Opponent.TakeDamage(damage);
                                    
                                    // 反撃処理
                                    if (x.Opponent.Status.passiveSkills.Contains(SkillAttributeName.Counter))
                                    {
                                        this.TakeDamage(Calculator.GetCounterDamage(damage));
                                    }

                                    // 吸収処理
                                    if (this.Status.passiveSkills.Contains(SkillAttributeName.Absorption))
                                    {
                                        this.RecoveryFixed(Calculator.GetAbsorptionRecoveryAmount(damage));
                                    }
                                    break;
                                case SkillAttributeName.BehaviourRecovery:
                                    this.RecoveryRate(Calculator.GetRecoveryRate(this.Status, skill));
                                    break;
                                case SkillAttributeName.BehaviourAddAbnormalStatus:
                                    var abnormalStatus = (AbnormalStatus)skill.attributes.Get(SkillAttributeName.AddAbnormalStatusType).value;
                                    if (Calculator.CanAddAbnormalStatus(abnormalStatus, x.Opponent.Status))
                                    {
                                        x.Opponent.Status.abnormalStatuses.Add(abnormalStatus);
                                    }
                                    break;
                                case SkillAttributeName.BehaviourRemoveAbnormalStatus:
                                    var abnormalStatusType = (AbnormalStatus)skill.attributes.Get(SkillAttributeName.RemoveAbnormalStatusType).value;
                                    if (!this.Status.abnormalStatuses.Contains(abnormalStatusType))
                                    {
                                        this.Status.abnormalStatuses.Remove(abnormalStatusType);
                                    }
                                    break;
                                default:
                                    Assert.IsTrue(false, $"{attribute.name}は未対応です");
                                    break;
                            }
                        }
                        
                        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));

                        // 相手が死亡していたら強制的に終了する
                        if (x.Opponent.Status.IsDead)
                        {
                            break;
                        }
                    }
                    
                    // 毒の処理
                    if (this.Status.abnormalStatuses.Contains(AbnormalStatus.Poison))
                    {
                        Debug.Log("TODO: 毒演出");
                        this.TakeDamage(Calculator.GetPoisonDamage(this.Status));
                        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
                    }
                })
                .AddTo(bag);
        }

        private void TakeDamage(int damage)
        {
            this.Status.hitPoint.Value -= damage;
        }

        private void RecoveryRate(float rate)
        {
            var recoveryAmount = Mathf.FloorToInt(this.Status.hitPointMax * rate);
            this.RecoveryFixed(recoveryAmount);
        }

        private void RecoveryFixed(int value)
        {
            var hitPoint = Mathf.Min(this.Status.hitPoint + value, this.Status.hitPointMax);
            this.Status.hitPoint.Value = hitPoint;
        }
    }
}
