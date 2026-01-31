using Cysharp.Threading.Tasks;
using KarenKrill.UniCore.Logging;
using KarenKrill.UniCore.StateSystem;
using KarenKrill.UniCore.StateSystem.Abstractions;
using KarenKrill.UniCore.Utilities;
using UnityEngine;
using Zenject;
using ArmyClash.GameFlow.Abstractions;
using ArmyClash.GameFlow;

namespace ArmyClash
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallLogging();
            InstallGameFlow();
        }
        private ILogger _logger;
        
        private void InstallLogging()
        {
#if DEBUG
            Container.Bind<ILogger>().To<Logger>().FromNew().AsSingle().WithArguments(new DebugLogHandler());
#else
            Container.Bind<ILogger>().To<StubLogger>().FromNew().AsSingle();
#endif
            UniTaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void InstallGameFlow()
        {
            Container.Bind<IStateMachine<GameState>>()
                .To<StateMachine<GameState>>()
                .AsSingle()
                .WithArguments(new GameStateGraph())
                .OnInstantiated((context, instance) =>
                {
                    if (instance is IStateMachine<GameState> stateMachine)
                    {
                        context.Container.Bind<IStateSwitcher<GameState>>().FromInstance(stateMachine.StateSwitcher);
                    }
                })
                .NonLazy();

            var stateHandlerTypes = ReflectionUtilities.GetInheritorTypes(typeof(IStateHandler<GameState>), System.Type.EmptyTypes);
            foreach (var stateHandlerType in stateHandlerTypes)
            {
                Container.BindInterfacesTo(stateHandlerType).AsSingle();
            }

            Container.BindInterfacesTo<ManagedStateMachine<GameState>>().AsSingle().OnInstantiated((context, target) =>
            {
                if (target is ManagedStateMachine<GameState> managedStateMachine)
                {
                    managedStateMachine.Start();
                }
            }).NonLazy();

            Container.BindInterfacesAndSelfTo<GameStateNavigator>().AsSingle();
        }
        
        private void OnApplicationQuit()
        {
            var gameStateNavigator = Container.Resolve<IGameStateNavigator>();
            if (gameStateNavigator.State != GameState.Exit)
            {
                gameStateNavigator.Exit();
            }
        }

        private void OnUnobservedTaskException(System.Exception ex)
        {
            _logger ??= Container.TryResolve<ILogger>();
            _logger?.LogError(nameof(ProjectInstaller), string.Format("Unobserved task exception {0}", ex));
        }
    }
}
