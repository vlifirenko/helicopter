using Apache.Model;
using Apache.Model.Audio;
using Apache.Model.Config;
using Apache.Service;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Apache.Ecs.System.Audio
{
    public class InitAudioService : IEcsInitSystem
    {
        private readonly EcsCustomInject<IAudioService> _audioService;
        private readonly EcsCustomInject<AudioConfig> _audioConfig;
        private readonly EcsCustomInject<SceneData> _sceneData;

        public void Init(IEcsSystems systems)
        {
            _audioService.Value.InitializeBuses();
            _audioService.Value.InitializeAmbience(_audioConfig.Value.ambience);
            _audioService.Value.InitializeMusic(_audioConfig.Value.music);

            InitUiVolumeControl();
        }

        private void InitUiVolumeControl()
        {
            var view = _sceneData.Value.CanvasView.VolumeControl;
            
            view.MasterSlider.onValueChanged.AddListener(value 
                => _audioService.Value.SetBusVolume(AudioBus.Master, value));
            view.MusicSlider.onValueChanged.AddListener(value 
                => _audioService.Value.SetBusVolume(AudioBus.Music, value));
            view.AmbienceSlider.onValueChanged.AddListener(value 
                => _audioService.Value.SetBusVolume(AudioBus.Ambience, value));
            view.SfxSlider.onValueChanged.AddListener(value 
                => _audioService.Value.SetBusVolume(AudioBus.SFX, value));
        }
    }
}