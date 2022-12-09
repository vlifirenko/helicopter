using Apache.View.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Apache.View.Ui
{
    public class UiVolumeControl : AUiView
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider ambienceSlider;
        [SerializeField] private Slider sfxSlider;

        public Slider MasterSlider => masterSlider;
        public Slider MusicSlider => musicSlider;
        public Slider AmbienceSlider => ambienceSlider;
        public Slider SfxSlider => sfxSlider;
    }
}