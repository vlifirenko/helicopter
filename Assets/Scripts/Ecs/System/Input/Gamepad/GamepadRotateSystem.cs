using Apache.Ecs.Component.Unit;
using Apache.Model;
using Apache.Model.Config;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Apache.Ecs.System.Input.Gamepad
{
    public class GamepadRotateSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent>, Exc<DeadComponent>> _filter;
        private readonly EcsPoolInject<UnitComponent> _unitPool;
        private readonly EcsPoolInject<RotationComponent> _rotationPool;
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsCustomInject<GameData> _gameData;
        private readonly EcsCustomInject<GlobalConfig> _commonConfig;

        public void Init(IEcsSystems systems)
        {
            _gameData.Value.GameInput.Player.Look.performed += context =>
            {
                var value = context.ReadValue<Vector2>();
                RotateToPoint(value);
            };
        }

        private void RotateToPoint(Vector3 direction)
        {
            foreach (var entity in _filter.Value)
            {
                var unit = _unitPool.Value.Get(entity);
                ref var rotation = ref _rotationPool.Value.Get(entity);
                
                direction = new Vector3(direction.x, 0f, direction.y);
                rotation.Direction = direction;
                rotation.Speed = unit.Config.rotationSpeed;
            }
        }
    }
}