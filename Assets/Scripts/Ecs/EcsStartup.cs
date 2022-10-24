using Apache.Ecs.Component.Unit;
using Apache.Ecs.System;
using Apache.Ecs.System.Game;
using Apache.Ecs.System.Unit;
using Apache.Model;
using Apache.Model.Config;
using Apache.Service;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Apache.Ecs
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneData sceneData;
        [SerializeField] private CommonConfig commonConfig;

        private EcsWorld _world;
        private IEcsSystems _systems;

        private GameData _gameData;

        private IUnitService _unitService;

        private void Start()
        {
            _gameData = new GameData();

            _world = new EcsWorld();

            _unitService = new UnitService(_world);

            _systems = new EcsSystems(_world);
            _systems

                // game
                .Add(new GameSystem())
                .Add(new InputSystem())
                .Add(new MouseTargetSystem())

                // unit    
                .Add(new InitUnitsSystem())
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new MouseRotateSystem())

                //
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(_gameData)
                .Inject(_unitService)
                .Inject(sceneData, commonConfig)
                //
                .Inject()
                .Init();
        }

        private void Update() => _systems?.Run();

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;

            _world?.Destroy();
            _world = null;
        }
    }
}