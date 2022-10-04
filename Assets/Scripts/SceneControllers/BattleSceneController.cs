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
    public sealed class BattleSceneController : SceneController
    {
        [SerializeField]
        private ActorStatusBuilder playerStatus;

        [SerializeField]
        private ActorStatusBuilder enemyStatus;
        
        public enum StateType
        {
            Invalid,
            Initialize,
            BattleStart,
            PlayerTurn,
            EnemyTurn,
            BattleEnd,
        }

        private Actor player;

        private Actor enemy;

        private readonly StateController<StateType> stateController = new(StateType.Invalid);

        protected override UniTask OnStartAsync(DisposableBagBuilder scope)
        {
            this.stateController.Set(StateType.Initialize, OnEnterInitialize, null);
            this.stateController.Set(StateType.BattleStart, OnEnterBattleStart, null);
            this.stateController.Set(StateType.PlayerTurn, OnEnterPlayerTurn, null);
            this.stateController.Set(StateType.EnemyTurn, OnEnterEnemyTurn, null);
            this.stateController.Set(StateType.BattleEnd, OnEnterBattleEnd, null);
            this.stateController.ChangeRequest(StateType.Initialize);
            return UniTask.CompletedTask;
        }

        protected override void OnInitializeMessageBroker(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<BattleEvent.StartBattle>();
            builder.AddMessageBroker<BattleEvent.Dispose>();
            builder.AddMessageBroker<Actor, BattleEvent.StartTurn>();
        }

        private void OnEnterInitialize(StateType prev)
        {
            Debug.Log("Initialize");
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
            
            this.stateController.ChangeRequest(StateType.BattleStart);
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
            GlobalMessagePipe.GetPublisher<BattleEvent.Dispose>()
                .Publish(BattleEvent.Dispose.Get());
        }
        
        private bool IsBattleEnd()
        {
            return this.player.Status.IsDead || this.enemy.Status.IsDead;
        }
    }
}
