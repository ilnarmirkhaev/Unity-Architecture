using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            IAssetProvider assetProvider = RegisterAssetProvider();
            IStaticDataService staticData = RegisterStaticDataService();
            IPersistentProgressService progressService = RegisterPersistentProgressService();
            IRandomService randomService = new RandomService();

            _services.RegisterSingle(randomService);
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(assetProvider, staticData, randomService, progressService));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private IPersistentProgressService RegisterPersistentProgressService()
        {
            var persistentProgressService = new PersistentProgressService();
            _services.RegisterSingle<IPersistentProgressService>(persistentProgressService);
            return persistentProgressService;
        }

        private IAssetProvider RegisterAssetProvider()
        {
            var assetProvider = new AssetProvider();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
            return assetProvider;
        }

        private IStaticDataService RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadStaticData();
            _services.RegisterSingle(staticData);
            return staticData;
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}