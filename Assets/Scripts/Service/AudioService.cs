using System.Collections.Generic;
using Apache.Model.Audio;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Apache.Service
{
    public class AudioService : IAudioService
    {
        private readonly List<EventInstance> _eventInstances = new List<EventInstance>();
        private readonly List<StudioEventEmitter> _eventEmitters = new List<StudioEventEmitter>();
        private EventInstance _ambienceEventInstance;
        private EventInstance _musicEventInstance;
        private readonly Dictionary<string, Bus> _buses = new Dictionary<string, Bus>();

        public void InitializeBuses()
        {
            _buses.Add(AudioBus.Master, RuntimeManager.GetBus(AudioBus.Master));
            _buses.Add(AudioBus.Music, RuntimeManager.GetBus(AudioBus.Music));
            _buses.Add(AudioBus.Ambience, RuntimeManager.GetBus(AudioBus.Ambience));
            _buses.Add(AudioBus.SFX, RuntimeManager.GetBus(AudioBus.SFX));
        }

        public void SetBusVolume(string bus, float value) => _buses[bus].setVolume(value);

        public void PlayOneShot(EventReference eventReference, Vector3 worldPosition)
            => RuntimeManager.PlayOneShot(eventReference, worldPosition);

        public void PlayOneShot(EventReference eventReference) => RuntimeManager.PlayOneShot(eventReference);

        public EventInstance CreateInstance(EventReference eventReference)
        {
            var eventInstance = RuntimeManager.CreateInstance(eventReference);
            _eventInstances.Add(eventInstance);

            return eventInstance;
        }

        public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
        {
            var emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
            emitter.EventReference = eventReference;
            _eventEmitters.Add(emitter);

            return emitter;
        }

        public void InitializeAmbience(EventReference eventReference)
        {
            _ambienceEventInstance = CreateInstance(eventReference);
            _ambienceEventInstance.start();
        }

        public void SetAmbienceParameter(string parameter, float value)
            => _ambienceEventInstance.setParameterByName(parameter, value);

        public void InitializeMusic(EventReference eventReference)
        {
            _musicEventInstance = CreateInstance(eventReference);
            _musicEventInstance.start();
        }

        public void SetMusicArea(float area) => _musicEventInstance.setParameterByName(AudioParameter.Area, area);

        public void Destroy()
        {
            foreach (var eventInstance in _eventInstances)
            {
                eventInstance.stop(STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }

            foreach (var emitter in _eventEmitters)
                emitter.Stop();
        }
    }
}