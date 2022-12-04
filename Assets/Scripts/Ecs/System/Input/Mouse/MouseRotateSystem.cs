using Apache.Ecs.Component.Unit;
using Apache.Model;
using Apache.Model.Config;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Apache.Ecs.System.Input.Mouse
{
    public class MouseRotateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent>, Exc<DeadComponent>> _filter;
        private readonly EcsPoolInject<UnitComponent> _unitPool;
        private readonly EcsPoolInject<RotationComponent> _rotationPool;
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsCustomInject<GlobalConfig> _commonConfig;

        public void Run(IEcsSystems systems)
        {
            if (_commonConfig.Value.isGamepad)
                return;
            
            foreach (var entity in _filter.Value)
            {
                var ray = _sceneData.Value.MainCamera.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _commonConfig.Value.groundLayer))
                {
                    RotateToPoint(hit.point);
                }
            }
        }

        private void RotateToPoint(Vector3 hit)
        {
            foreach (var entity in _filter.Value)
            {
                var unit = _unitPool.Value.Get(entity);
                ref var rotation = ref _rotationPool.Value.Get(entity);
                var direction = new Vector3(hit.x, unit.View.Transform.position.y, hit.z) - unit.View.Transform.position;

                rotation.Direction = direction;
                rotation.Speed = unit.Config.rotationSpeed;
            }
        }
    }
}