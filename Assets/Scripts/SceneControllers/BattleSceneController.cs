using System;
using Cookie.UISystems;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MessagePipe;
using UnityEngine;

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
            Finalize,
        }

        private Actor player;

        private Actor enemy;

        private readonly StateController<StateType> stateController = new(StateType.Invalid);

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

            var uiView = UIManager.Open(this.battleUIPrefab);
            StartObserveActorStatusView(this.player, uiView.PlayerStatusView)
                .AddTo(scope);
            StartObserveActorStatusView(this.enemy, uiView.EnemyStatusView)
                .AddTo(scope);

            this.stateController.Set(StateType.BattleStart, OnEnterBattleStart, null);
            this.stateController.Set(StateType.PlayerTurn, OnEnterPlayerTurn, null);
            this.stateController.Set(StateType.EnemyTurn, OnEnterEnemyTurn, null);
            this.stateController.Set(StateType.BattleEnd, OnEnterBattleEnd, null);
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
                    uiView.DamageLabelUIView.Create(x.Damage, x.Actor.ActorType);
                })
                .AddTo(scope);

            uiView.EnemyImage.enabled = false;
            var enemySprite = await AssetLoader.LoadAsync<Sprite>($"Assets/Textures/Enemy/{this.enemy.Status.spriteId}.jpg");
            uiView.EnemyImage.sprite = enemySprite;
            uiView.EnemyImage.enabled = true;
            
            this.stateController.ChangeRequest(StateType.BattleStart);
        }

        protected override void OnInitializeMessageBroker(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<BattleEvent.StartBattle>();
            builder.AddMessageBroker<BattleEvent.Dispose>();
            builder.AddMessageBroker<Actor, BattleEvent.StartTurn>();
            builder.AddMessageBroker<BattleEvent.TakedDamage>();
        }
        
        private void OnEnterBattleStart(StateType prev)
        {
            Debug.Log("BattleStart");
            this.MessageBroker.GetPublisher<BattleEvent.StartBattle>()
                .Publish(BattleEvent.StartBattle.Get());
            this.stateController.ChangeRequest(this.player.Status.speed >= this.enemy.Status.speed ? StateType.PlayerTurn : StateType.EnemyTurn);
        }

        private async void OnEnterPlayerTurn(StateType prev)
        {
            await this.MessageBroker.GetAsyncPublisher<Actor, BattleEvent.StartTurn>()
                .PublishAsync(this.player, BattleEvent.StartTurn.Get(this.enemy));

            this.stateController.ChangeRequest(IsBattleEnd() ? StateType.BattleEnd : StateType.EnemyTurn);
        }

        private async void OnEnterEnemyTurn(StateType prev)
        {
            await this.MessageBroker.GetAsyncPublisher<Actor, BattleEvent.StartTurn>()
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

            view.ActorName.text = actor.Status.Name;
            
            return bag.Build();
        }
    }
}
