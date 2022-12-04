using Apache.Ecs.Component.Unit;
using Apache.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Apache.Ecs.System.Input
{
    public class InputMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent>, Exc<DeadComponent>> _filter;
        private readonly EcsCustomInject<GameData> _gameData;
        private readonly EcsPoolInject<MovementComponent> _movementPool;
        private readonly EcsPoolInject<UnitComponent> _unitPool;

        public void Run(IEcsSystems systems)
        {
            var move = _gameData.Value.GameInput.Player.Move.ReadValue<Vector2>();
            OnMove(move);
        }

        private void OnMove(Vector2 value)
        {
            foreach (var entity in _filter.Value)
            {
                var unit = _unitPool.Value.Get(entity);
                var direction = new Vector3(value.x, 0f, value.y);

                _movementPool.Value.Get(entity) = new MovementComponent
                {
                    Direction = direction,
                    Speed = unit.Config.moveSpeed
                };
            }
        }
    }
}