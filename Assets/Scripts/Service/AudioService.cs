﻿using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Apache.Service
{
    public class AudioService : IAudioService
    {
        private readonly List<EventInstance> _eventInstances = new List<EventInstance>();
        private readonly List<StudioEventEmitter> _eventEmitters = new List<StudioEventEmitter>();

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