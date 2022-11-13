using System;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MessagePipe;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleSceneController : SceneController
    {
        [SerializeField]
        private BattleUIView battleUIPrefab;

        [SerializeField]
        private ActorStatusBuilder playerStatus;

        [SerializeField]
        private ActorStatusBuilder enemyStatus;
        
        public enum StateType
        {
            Invalid,
            BattleStart,
            PlayerTurn,
            EnemyTurn,
            BattleEnd,
            Escape,
            Finalize,
        }

        private Actor player;

        private Actor enemy;

        private BattleUIView uiView;

        private readonly StateController<StateType> stateController = new(StateType.Invalid);

        private readonly BattleResourceManager battleResourceManager = new();

        protected override async UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            if (SceneMediator.IsMatchArgument<BattleSceneArgument>())
            {
                var argument = SceneMediator.GetArgument<BattleSceneArgument>();
                this.player = new Actor(ActorType.Player, argument.playerStatusBuilder.Create(), this.MessageBroker);
                this.enemy = new Actor(ActorType.Enemy, argument.enemyStatusBuilder.Create(), this.MessageBroker);
            }
            else
            {
                this.player = new Actor(ActorType.Player, this.playerStatus.Create(), this.MessageBroker);
                this.enemy = new Actor(ActorType.Enemy, this.enemyStatus.Create(), this.MessageBroker);
            }

            this.uiView = UIManager.Open(this.battleUIPrefab);
            StartObserveActorStatusView(this.player, uiView.PlayerStatusView)
                .AddTo(scope);
            StartObserveActorStatusView(this.enemy, uiView.EnemyStatusView)
                .AddTo(scope);

            this.stateController.Set(StateType.BattleStart, OnEnterBattleStart, null);
            this.stateController.Set(StateType.PlayerTurn, OnEnterPlayerTurn, null);
            this.stateController.Set(StateType.EnemyTurn, OnEnterEnemyTurn, null);
            this.stateController.Set(StateType.BattleEnd, OnEnterBattleEnd, null);
            this.stateController.Set(StateType.Escape, OnEnterEscape, null);
            this.stateController.Set(StateType.Finalize, OnEnterBattleFinalize, null);

            this.MessageBroker.GetSubscriber<SceneEvent.OnDestroy>()
                .Subscribe(_ =>
                {
                    UIManager.Close(uiView);
                })
                .AddTo(scope);

            this.MessageBroker.GetSubscriber<BattleEvent.TakedDamage>()
                .Subscribe(x =>
                {
                    uiView.DamageLabelUIView.CreateDamageLabel(x.DamageData.damage, x.Actor.ActorType).Forget();

                    if (x.Actor.ActorType == ActorType.Enemy)
                    {
                        if (x.Actor.Status.IsDead)
                        {
                            uiView.EnemyImageUIView.PlayDiedAsync().Forget();
                        }
                        else
                        {
                            uiView.EnemyImageUIView.PlayDamageAsync().Forget();
                        }
                    }
                    else
                    {
                        uiView.PlayerStatusView.PlayDamageAsync().Forget();
                    }
                })
                .AddTo(scope);

            this.MessageBroker.GetSubscriber<BattleEvent.AddedAbnormalStatus>()
                .Subscribe(x =>
                {
                    if (x.Actor.ActorType == ActorType.Enemy)
                    {
                        Debug.Log("TODO 敵の状態異常付与エフェクト");
                    }
                    else
                    {
                        var icon = this.battleResourceManager.AbnormalStatusIcons[x.AbnormalStatus];
                        uiView.PlayerStatusView.AddAbnormalStatusIcon(x.AbnormalStatus, icon);
                    }
                })
                .AddTo(scope);

            this.MessageBroker.GetSubscriber<BattleEvent.RemovedAbnormalStatus>()
                .Subscribe(x =>
                {
                    if (x.Actor.ActorType == ActorType.Enemy)
                    {
                        Debug.Log("TODO 敵の状態異常解除エフェクト");
                    }
                    else
                    {
                        uiView.PlayerStatusView.RemoveAbnormalStatusIcon(x.AbnormalStatus);
                    }
                })
                .AddTo(scope);

            this.MessageBroker.GetSubscriber<BattleEvent.AttackDeclaration>()
                .Subscribe(x =>
                {
                    uiView.AttackDeclarationUIView.Create(x.Message, x.Actor.ActorType);
                })
                .AddTo(scope);

            this.MessageBroker.GetSubscriber<BattleEvent.GivedDamage>()
                .Subscribe(x =>
                {
                    var effectId = x.ActiveSkill.attributes.Get(SkillAttributeName.EffectId);
                    uiView.BattleEffectUIView.Create(effectId.value, x.Target.ActorType).Forget();

                    if (x.DamageData.isCritical)
                    {
                        uiView.CriticalEffectUIView.Play();
                    }
                })
                .AddTo(scope);

            this.MessageBroker.GetSubscriber<BattleEvent.Recovered>()
                .Subscribe(x =>
                {
                    uiView.DamageLabelUIView.CreateRecoveryLabel(x.Value, x.Actor.ActorType).Forget();
                })
                .AddTo(scope);
            
            this.uiView.BattleHeaderUIView.EscapeButton.Button.onClick.AddListener(async () =>
            {
                Time.timeScale = 0.0f;
                var result = await UIManager.PopupUIController.ShowAsync(LocalizeString.Get("UI", "ConfirmEscape"));
                if (result)
                {
                    this.stateController.ChangeRequest(StateType.Escape);
                }
                else
                {
                    SetBattleSpeed(UserData.current.battleSpeedType);
                }
            });
            
            this.uiView.BattleHeaderUIView.SpeedButton.Button.onClick.AddListener(() =>
            {
                var userData = UserData.current;
                userData.battleSpeedType = userData.battleSpeedType.GetNext();
                SaveData.SaveUserData(userData);
                SetBattleSpeed(userData.battleSpeedType);
                this.uiView.BattleHeaderUIView.SetSpeedButtonMessage(userData.battleSpeedType);
            });
            
            SetBattleSpeed(UserData.current.battleSpeedType);
            this.uiView.BattleHeaderUIView.SetSpeedButtonMessage(UserData.current.battleSpeedType);

            await this.battleResourceManager.SetupAsync(scope);
            await uiView.EnemyImageUIView.SetupAsync(this.enemy.Status.spriteId);
            
            this.stateController.ChangeRequest(StateType.BattleStart);
        }

        protected override void OnInitializeMessageBroker(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<BattleEvent.StartBattle>();
            builder.AddMessageBroker<BattleEvent.Dispose>();
            builder.AddMessageBroker<Actor, BattleEvent.StartTurn>();
            builder.AddMessageBroker<BattleEvent.TakedDamage>();
            builder.AddMessageBroker<BattleEvent.AddedAbnormalStatus>();
            builder.AddMessageBroker<BattleEvent.RemovedAbnormalStatus>();
            builder.AddMessageBroker<BattleEvent.AttackDeclaration>();
            builder.AddMessageBroker<BattleEvent.GivedDamage>();
            builder.AddMessageBroker<BattleEvent.Recovered>();
        }
        
        private async void OnEnterBattleStart(StateType prev, DisposableBagBuilder scope)
        {
            this.MessageBroker.GetPublisher<BattleEvent.StartBattle>()
                .Publish(BattleEvent.StartBattle.Get());

            await UniTask.WhenAll(
                this.uiView.EnemyImageUIView.PlayAppearanceAsync(),
                this.uiView.BattleMessageUIView.Play(BattleMessageUIView.Type.BattleStart)
                );
            
            this.stateController.ChangeRequest(this.player.Status.speed >= this.enemy.Status.speed ? StateType.PlayerTurn : StateType.EnemyTurn);
        }

        private async void OnEnterPlayerTurn(StateType prev, DisposableBagBuilder scope)
        {
            var cts = new CancellationTokenDisposable();
            scope.Add(cts);
            try
            {
                await this.MessageBroker.GetAsyncPublisher<Actor, BattleEvent.StartTurn>()
                    .PublishAsync(this.player, BattleEvent.StartTurn.Get(this.enemy), cts.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            this.stateController.ChangeRequest(IsBattleEnd() ? StateType.BattleEnd : StateType.EnemyTurn);
        }

        private async void OnEnterEnemyTurn(StateType prev, DisposableBagBuilder scope)
        {
            var cts = new CancellationTokenDisposable();
            scope.Add(cts);
            try
            {
                await this.MessageBroker.GetAsyncPublisher<Actor, BattleEvent.StartTurn>()
                    .PublishAsync(this.enemy, BattleEvent.StartTurn.Get(this.player), cts.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            this.stateController.ChangeRequest(IsBattleEnd() ? StateType.BattleEnd : StateType.PlayerTurn);
        }

        private async void OnEnterBattleEnd(StateType prev, DisposableBagBuilder scope)
        {
            var judgement = !this.player.Status.IsDead ? BattleJudgement.PlayerWin : BattleJudgement.PlayerLose;

            await this.uiView.BattleMessageUIView.Play(
                judgement == BattleJudgement.PlayerWin
                    ? BattleMessageUIView.Type.Win
                    : BattleMessageUIView.Type.Lose
                );

            if (SceneMediator.IsMatchArgument<BattleSceneArgument>())
            {
                var argument = SceneMediator.GetArgument<BattleSceneArgument>();
                argument.onBattleEnd?.Invoke(judgement);
            }
            this.stateController.ChangeRequest(StateType.Finalize);
        }

        private void OnEnterEscape(StateType prev, DisposableBagBuilder scope)
        {
            if (SceneMediator.IsMatchArgument<BattleSceneArgument>())
            {
                var argument = SceneMediator.GetArgument<BattleSceneArgument>();
                argument.onBattleEnd?.Invoke(BattleJudgement.PlayerLose);
            }
            this.stateController.ChangeRequest(StateType.Finalize);
        }
        
        private void OnEnterBattleFinalize(StateType prev, DisposableBagBuilder scope)
        {
            Debug.Log("Finalize");
            SetBattleSpeed(BattleSpeedType.Lv_1);
            this.MessageBroker.GetPublisher<BattleEvent.Dispose>()
                .Publish(BattleEvent.Dispose.Get());
            
            if (SceneMediator.IsMatchArgument<BattleSceneArgument>())
            {
                var argument = SceneMediator.GetArgument<BattleSceneArgument>();
                argument.onBattleFinalize?.Invoke();
            }
        }
        
        private bool IsBattleEnd()
        {
            return this.player.Status.IsDead || this.enemy.Status.IsDead;
        }

        private static IDisposable StartObserveActorStatusView(Actor actor, ActorStatusView view)
        {
            var bag = DisposableBag.CreateBuilder();
            void UpdateHitPointSlider()
            {
                view.HitPointSlider.value = (float)actor.Status.hitPoint / actor.Status.hitPointMax;
            }
            
            actor.Status.hitPoint
                .Queue()
                .Subscribe(_ =>
                {
                    UpdateHitPointSlider();
                })
                .AddTo(bag);

            actor.Status.hitPointMax
                .Queue()
                .Subscribe(_ =>
                {
                    UpdateHitPointSlider();
                })
                .AddTo(bag);

            view.ActorName.text = actor.Status.Name;
#if DEBUG
            if (actor.ActorType == ActorType.Enemy)
            {
                view.ActorName.text += $"(Lv.{actor.Status.playerLevel})";
            }
#endif
            
            return bag.Build();
        }

        private static void SetBattleSpeed(BattleSpeedType battleSpeedType)
        {
            var speed = 1.0f;

            switch (battleSpeedType)
            {
                case BattleSpeedType.Lv_1:
                    speed = 1.0f;
                    break;
                case BattleSpeedType.Lv_2:
                    speed = 2.0f;
                    break;
                case BattleSpeedType.Lv_3:
                    speed = 3.0f;
                    break;
                default:
                    Assert.IsTrue(false, $"{battleSpeedType}は未対応です");
                    break;
            }

            Time.timeScale = speed;
        }
    }
}
