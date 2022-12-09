using Apache.Model.Audio;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Apache.Service
{
    public interface IAudioService : IDestroyable
    {
        public void InitializeBuses();

        public void SetBusVolume(string bus, float value);
        
        public void PlayOneShot(EventReference eventReference, Vector3 worldPosition);
        
        public void PlayOneShot(EventReference eventReference);

        public EventInstance CreateInstance(EventReference eventReference);

        public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject);

        public void InitializeAmbience(EventReference eventReference);

        public void SetAmbienceParameter(string parameter, float value);

        public void InitializeMusic(EventReference eventReference);

        public void SetMusicArea(float area);
    }
}