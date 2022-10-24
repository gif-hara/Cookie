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
        private Transform uiParent;

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
            Finalize,
        }

        private Actor player;

        private Actor enemy;

        private readonly StateController<StateType> stateController = new(StateType.Invalid);

        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            if (SceneMediator.IsMatchArgument<BattleSceneArgument>())
            {
                var argument = SceneMediator.GetArgument<BattleSceneArgument>();
                this.player = new Actor(ActorType.Player, argument.playerStatusBuilder.Create());
                this.enemy = new Actor(ActorType.Enemy, argument.enemyStatusBuilder.Create());
            }
            else
            {
                this.player = new Actor(ActorType.Player, this.playerStatus.Create());
                this.enemy = new Actor(ActorType.Enemy, this.enemyStatus.Create());
            }

            var uiView = Instantiate(this.battleUIPrefab, this.uiParent);
            StartObserveActorStatusView(this.player, uiView.PlayerStatusView)
                .AddTo(scope);
            StartObserveActorStatusView(this.enemy, uiView.EnemyStatusView)
                .AddTo(scope);

            this.stateController.Set(StateType.BattleStart, OnEnterBattleStart, null);
            this.stateController.Set(StateType.PlayerTurn, OnEnterPlayerTurn, null);
            this.stateController.Set(StateType.EnemyTurn, OnEnterEnemyTurn, null);
            this.stateController.Set(StateType.BattleEnd, OnEnterBattleEnd, null);
            this.stateController.Set(StateType.Finalize, OnEnterBattleFinalize, null);
            this.stateController.ChangeRequest(StateType.BattleStart);
            return UniTask.CompletedTask;
        }

        protected override void OnInitializeMessageBroker(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<BattleEvent.StartBattle>();
            builder.AddMessageBroker<BattleEvent.Dispose>();
            builder.AddMessageBroker<Actor, BattleEvent.StartTurn>();
        }
        
        private void OnEnterBattleStart(StateType prev)
        {
            Debug.Log("BattleStart");
            GlobalMessagePipe.GetPublisher<BattleEvent.StartBattle>()
                .Publish(BattleEvent.StartBattle.Get());
            this.stateController.ChangeRequest(this.player.Status.speed >= this.enemy.Status.speed ? StateType.PlayerTurn : StateType.EnemyTurn);
        }

        private async void OnEnterPlayerTurn(StateType prev)
        {
            await GlobalMessagePipe.GetAsyncPublisher<Actor, BattleEvent.StartTurn>()
                .PublishAsync(this.player, BattleEvent.StartTurn.Get(this.enemy));

            this.stateController.ChangeRequest(IsBattleEnd() ? StateType.BattleEnd : StateType.EnemyTurn);
        }

        private async void OnEnterEnemyTurn(StateType prev)
        {
            await GlobalMessagePipe.GetAsyncPublisher<Actor, BattleEvent.StartTurn>()
                .PublishAsync(this.enemy, BattleEvent.StartTurn.Get(this.player));
            
            this.stateController.ChangeRequest(IsBattleEnd() ? StateType.BattleEnd : StateType.PlayerTurn);
        }

        private void OnEnterBattleEnd(StateType prev)
        {
            Debug.Log("BattleEnd");
            var judgement = !this.player.Status.IsDead ? BattleJudgement.PlayerWin : BattleJudgement.PlayerLose;

            if (SceneMediator.IsMatchArgument<BattleSceneArgument>())
            {
                var argument = SceneMediator.GetArgument<BattleSceneArgument>();
                argument.onBattleEnd?.Invoke(judgement);
            }
            this.stateController.ChangeRequest(StateType.Finalize);
        }
        
        private void OnEnterBattleFinalize(StateType prev)
        {
            Debug.Log("Finalize");
            GlobalMessagePipe.GetPublisher<BattleEvent.Dispose>()
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
                .Subscribe(x =>
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
            
            return bag.Build();
        }
    }
}
