using Apache.Ecs.Component.Unit;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Apache.Ecs.System.Unit
{
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitComponent, MovementComponent>> _filter;
        private readonly EcsPoolInject<UnitComponent> _unitPool;
        private readonly EcsPoolInject<MovementComponent> _movementPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var unit = _unitPool.Value.Get(entity);
                var rigidbody = unit.View.Rigidbody;
                var movement = _movementPool.Value.Get(entity);
                var velocity = movement.Direction * movement.Speed * Time.deltaTime;

                rigidbody.AddForce(velocity);
            }
        }
    }
}