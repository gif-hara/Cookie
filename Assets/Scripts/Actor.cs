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

        private MessageBroker messageBroker;

        public ActorType ActorType => this.actorType;

        public Actor(ActorType actorType, ActorStatus status, MessageBroker messageBroker)
        {
            this.actorType = actorType;
            this.Status = status;
            this.messageBroker = messageBroker;

            var bag = DisposableBag.CreateBuilder();

            messageBroker.GetSubscriber<BattleEvent.Dispose>()
                .Subscribe(_ =>
                {
                    bag.Build().Dispose();
                })
                .AddTo(bag);
            
            messageBroker.GetAsyncSubscriber<Actor, BattleEvent.StartTurn>()
                .Subscribe(this, async (x, cancelToken) =>
                {
                    foreach (var skill in this.Status.activeSkills)
                    {
                        // 麻痺の処理
                        if (this.Status.abnormalStatuses.Contains(AbnormalStatus.Paralysis) && Calculator.CanInvokeParalysis())
                        {
                            Debug.Log("TODO 麻痺演出");
                            await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
                            continue;
                        }
                        
                        messageBroker.GetPublisher<BattleEvent.AttackDeclaration>()
                            .Publish(BattleEvent.AttackDeclaration.Get(this, skill.Name));
                        
                        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken:cancelToken);

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
                                {
                                    void InvokeAttack()
                                    {
                                        var damageData = Calculator.GetDamageData(this.Status, skill, x.Opponent.Status);
                                        x.Opponent.TakeDamage(damageData);
                                        messageBroker.GetPublisher<BattleEvent.GivedDamage>()
                                            .Publish(BattleEvent.GivedDamage.Get(this, x.Opponent, damageData, skill));
                                    
                                        // 反撃処理
                                        if (x.Opponent.Status.passiveSkills.Contains(SkillAttributeName.Counter))
                                        {
                                            this.TakeDamage(Calculator.GetCounterDamage(damageData.damage, damageData.attackAttribute));
                                        }

                                        // 吸収処理
                                        if (this.Status.passiveSkills.Contains(SkillAttributeName.Absorption))
                                        {
                                            this.RecoveryFixed(Calculator.GetAbsorptionRecoveryAmount(damageData.damage));
                                        }
                                    }
                                    InvokeAttack();
                                    
                                    // 連続攻撃処理
                                    if (Calculator.CanInvokeContinuousAttack(this.Status))
                                    {
                                        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
                                        messageBroker.GetPublisher<BattleEvent.AttackDeclaration>()
                                            .Publish(BattleEvent.AttackDeclaration.Get(this, LocalizeString.Get("UI", "ContinuousAttack")));
                                        InvokeAttack();
                                    }
                                    break;
                                }
                                case SkillAttributeName.BehaviourRecovery:
                                {
                                    var value = this.RecoveryRate(Calculator.GetRecoveryRate(this.Status, skill));
                                    messageBroker.GetPublisher<BattleEvent.Recovered>()
                                        .Publish(BattleEvent.Recovered.Get(this, value));
                                    break;
                                }
                                case SkillAttributeName.BehaviourAddAbnormalStatus:
                                {
                                    var abnormalStatusType = (AbnormalStatus)skill.attributes.Get(SkillAttributeName.AddAbnormalStatusType).value;
                                    if (Calculator.CanAddAbnormalStatus(abnormalStatusType, this.Status, x.Opponent.Status))
                                    {
                                        x.Opponent.Status.abnormalStatuses.Add(abnormalStatusType);
                                        messageBroker.GetPublisher<BattleEvent.AddedAbnormalStatus>()
                                            .Publish(BattleEvent.AddedAbnormalStatus.Get(x.Opponent, abnormalStatusType));
                                    }
                                    break;
                                }
                                case SkillAttributeName.BehaviourRemoveAbnormalStatus:
                                {
                                    foreach (var a in skill.attributes.GetAll(SkillAttributeName.RemoveAbnormalStatusType))
                                    {
                                        var abnormalStatusType = (AbnormalStatus)a.value;
                                        if (this.Status.abnormalStatuses.Contains(abnormalStatusType))
                                        {
                                            this.Status.abnormalStatuses.Remove(abnormalStatusType);
                                            messageBroker.GetPublisher<BattleEvent.RemovedAbnormalStatus>()
                                                .Publish(BattleEvent.RemovedAbnormalStatus.Get(this, abnormalStatusType));
                                        }
                                    }
                                    break;
                                }
                                case SkillAttributeName.BehaviourAddBuff:
                                {
                                    foreach (var a in skill.attributes.GetAll(SkillAttributeName.AddBuffType))
                                    {
                                        var addBuffType = (AddBuffType)a.value;
                                        switch (addBuffType)
                                        {
                                            case AddBuffType.PhysicalStrength:
                                            {
                                                this.Status.physicalStrengthBuffLevel = Mathf.Min(this.Status.physicalStrengthBuffLevel + 1, Define.BuffLevelMax);
                                                break;
                                            }
                                            case AddBuffType.MagicStrength:
                                            {
                                                this.Status.magicStrengthBuffLevel = Mathf.Min(this.Status.magicStrengthBuffLevel + 1, Define.BuffLevelMax);
                                                break;
                                            }
                                            case AddBuffType.PhysicalDefense:
                                            {
                                                this.Status.physicalDefenseBuffLevel = Mathf.Min(this.Status.physicalDefenseBuffLevel + 1, Define.BuffLevelMax);
                                                break;
                                            }
                                            case AddBuffType.MagicDefense:
                                            {
                                                this.Status.magicDefenseBuffLevel = Mathf.Min(this.Status.magicDefenseBuffLevel + 1, Define.BuffLevelMax);
                                                break;
                                            }
                                            default:
                                                Assert.IsTrue(false, $"{addBuffType}は未対応です");
                                                break;
                                        }
                                    }
                                    break;
                                }
                                default:
                                    Assert.IsTrue(false, $"{attribute.name}は未対応です");
                                    break;
                            }
                        }
                        
                        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken:cancelToken);

                        // 相手が死亡していたら強制的に終了する
                        if (x.Opponent.Status.IsDead)
                        {
                            break;
                        }
                    }
                    
                    // 毒の処理
                    if (this.Status.abnormalStatuses.Contains(AbnormalStatus.Poison) && !x.Opponent.Status.IsDead)
                    {
                        Debug.Log("TODO: 毒演出");
                        this.TakeDamage(Calculator.GetPoisonDamage(this.Status));
                        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken:cancelToken);
                    }
                })
                .AddTo(bag);
        }

        private void TakeDamage(DamageData damageData)
        {
            this.Status.hitPoint.Value -= damageData.damage;
            this.messageBroker.GetPublisher<BattleEvent.TakedDamage>()
                .Publish(BattleEvent.TakedDamage.Get(this, damageData));
        }

        private int RecoveryRate(float rate)
        {
            var recoveryAmount = Mathf.FloorToInt(this.Status.hitPointMax * rate);
            this.RecoveryFixed(recoveryAmount);

            return recoveryAmount;
        }

        private void RecoveryFixed(int value)
        {
            var hitPoint = Mathf.Min(this.Status.hitPoint + value, this.Status.hitPointMax);
            this.Status.hitPoint.Value = hitPoint;
        }
    }
}
