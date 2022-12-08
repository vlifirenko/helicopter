using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Apache.Service
{
    public interface IAudioService : IDestroyable
    {
        public void PlayOneShot(EventReference eventReference, Vector3 worldPosition);
        
        public void PlayOneShot(EventReference eventReference);

        public EventInstance CreateInstance(EventReference eventReference);

        public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject);

        public void InitializeAmbience(EventReference eventReference);

        public void SetAmbienceParameter(string parameter, float value);
    }
}