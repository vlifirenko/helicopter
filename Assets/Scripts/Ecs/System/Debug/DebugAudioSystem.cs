using Apache.Model;
using Apache.Service;
using Apache.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Apache.Ecs.System.Debug
{
    public class DebugAudioSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsCustomInject<GameData> _gameData;
        private readonly EcsCustomInject<IAudioService> _audioService;

        public void Init(IEcsSystems systems)
        {
            var input = _gameData.Value.GameInput;

            input.Debug.Enable();

            input.Debug.F1.performed += context => OnF1();
            input.Debug.F2.performed += context => OnF2();
            input.Debug.F3.performed += context => OnF3();
        }

        private void OnF1()
        {
            _audioService.Value.SetAmbienceParameter(AudioParameter.WindIntensity, 1f);
        }

        private void OnF2()
        {
            _audioService.Value.SetAmbienceParameter(AudioParameter.WindIntensity, .5f);
        }

        private void OnF3()
        {
            _audioService.Value.SetAmbienceParameter(AudioParameter.WindIntensity, 0f);
        }

        public void Destroy(IEcsSystems systems) => _gameData.Value.GameInput.Disable();
    }
}