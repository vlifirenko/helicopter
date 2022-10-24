using Apache.Ecs.Component.Unit;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Apache.Ecs.System.Unit
{
    public class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitComponent, RotationComponent>> _filter;
        private readonly EcsPoolInject<UnitComponent> _unitPool;
        private readonly EcsPoolInject<RotationComponent> _rotationPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var rotation = _rotationPool.Value.Get(entity);
                if (rotation.Direction == Vector3.zero)
                    return;

                var unit = _unitPool.Value.Get(entity);

                unit.View.Transform.rotation = Quaternion.Slerp(
                    unit.View.Transform.rotation,
                    Quaternion.LookRotation(rotation.Direction, unit.View.Transform.up),
                    Time.deltaTime * rotation.Speed);
            }
        }
    }
}