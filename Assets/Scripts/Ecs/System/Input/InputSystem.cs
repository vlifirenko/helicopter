using Apache.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Apache.Ecs.System.Input
{
    public class InputSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsCustomInject<GameData> _gameData;

        public void Init(IEcsSystems systems)
        {
            _gameData.Value.GameInput = new GameInput();
            _gameData.Value.GameInput.Player.Enable();
        }

        public void Destroy(IEcsSystems systems) => _gameData.Value.GameInput.Player.Disable();
    }
}