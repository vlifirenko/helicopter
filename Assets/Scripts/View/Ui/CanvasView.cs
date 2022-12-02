using UnityEngine;

namespace Apache.View.Ui
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private UiTargetsContainer targets;

        public Canvas Canvas => canvas;
        public UiTargetsContainer Targets => targets;
    }
}