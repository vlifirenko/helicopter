using FMODUnity;
using UnityEngine;

namespace Apache.Model.Config
{
    [CreateAssetMenu(fileName = "Audio", menuName = "Config/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        public EventReference targetFound;
        public EventReference cannonShoot;
        public EventReference ambience;
        public EventReference music;
    }
}