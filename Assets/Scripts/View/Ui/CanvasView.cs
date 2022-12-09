using UnityEngine;

namespace Apache.View.Ui
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private UiTargetsContainer targets;
        [SerializeField] private UiVolumeControl volumeControl;

        public Canvas Canvas => canvas;
        public UiTargetsContainer Targets => targets;
        public UiVolumeControl VolumeControl => volumeControl;
    }
}