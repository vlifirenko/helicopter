using Apache.Ecs.Component.Unit;
using Apache.Ecs.System;
using Apache.Ecs.System.Debug;
using Apache.Ecs.System.Game;
using Apache.Ecs.System.Input;
using Apache.Ecs.System.Input.Gamepad;
using Apache.Ecs.System.Input.Mouse;
using Apache.Ecs.System.Ui;
using Apache.Ecs.System.Unit;
using Apache.Ecs.System.Weapon;
using Apache.Model;
using Apache.Model.Config;
using Apache.Service;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using UnityEngine.Serialization;

namespace Apache.Ecs
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneData sceneData;
        [SerializeField] private GlobalConfig globalConfig;
        [SerializeField] private WeaponConfig weaponConfig;
        [SerializeField] private AudioConfig audioConfig;

        private EcsWorld _world;
        private IEcsSystems _systems;

        private GameData _gameData;

        private IUnitService _unitService;
        private IAudioService _audioService;

        private void Start()
        {
            _gameData = new GameData();

            _world = new EcsWorld();

            _unitService = new UnitService(_world);
            _audioService = new AudioService();

            _systems = new EcsSystems(_world);
            _systems

                // game
                .Add(new GameSystem())
                // audio
                .Add(new InitAudioService())
                // input
                .Add(new InputMovementSystem())
                .Add(new MouseTargetSystem())
                .Add(new GamepadRotateSystem())
                .Add(new MouseRotateSystem())

                // unit    
                .Add(new InitUnitsSystem())
                // movement
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                // target
                .Add(new LookingTargetSystem())
                // weapon
                .Add(new ShootCannonSystem())

                // ui
                .Add(new UiShowTargetsSystem())

                //
#if UNITY_EDITOR
                // debug
                .Add(new DebugAudioSystem())
                //
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(_gameData)
                .Inject(_unitService, _audioService)
                .Inject(sceneData, globalConfig, weaponConfig, audioConfig)
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

            _audioService.Destroy();
        }
    }
}