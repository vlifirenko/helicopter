using Apache.Model;
using Apache.Model.Config;
using Apache.Service;
using FMOD.Studio;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using NotImplementedException = System.NotImplementedException;

namespace Apache.Ecs.System.Weapon
{
    public class ShootCannonSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<GameData> _gameData;
        private readonly EcsCustomInject<IAudioService> _audioService;
        private readonly EcsCustomInject<AudioConfig> _audioConfig;

        private EventInstance _sfxEventInstance;

        public void Init(IEcsSystems systems)
        {
            var gameInput = _gameData.Value.GameInput;

            gameInput.Player.Cannon.started += ctx => StartShooting();
            gameInput.Player.Cannon.canceled += ctx => StopShooting();

            _sfxEventInstance = _audioService.Value.CreateInstance(_audioConfig.Value.cannonShoot);
        }

        private void StartShooting()
        {
            _sfxEventInstance.getPlaybackState(out var playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                _sfxEventInstance.start();
        }

        private void StopShooting()
        {
            _sfxEventInstance.getPlaybackState(out var playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.PLAYING))
                _sfxEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}