using Apache.Model.Config;
using Apache.Service;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Apache.Ecs.System.Game
{
    public class InitAudioService : IEcsInitSystem
    {
        private readonly EcsCustomInject<IAudioService> _audioService;
        private readonly EcsCustomInject<AudioConfig> _audioConfig;
        
        public void Init(IEcsSystems systems)
        {
            _audioService.Value.InitializeAmbience(_audioConfig.Value.ambience);
        }
    }
}