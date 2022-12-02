using Apache.View;
using Apache.View.Core;
using Apache.View.Ui;
using UnityEngine;

namespace Apache.Model
{
    public class SceneData : AView
    {
        [SerializeField] private ApacheView apache;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CanvasView canvasView;

        public ApacheView Apache => apache;
        public Camera MainCamera => mainCamera;
        public CanvasView CanvasView => canvasView;
    }
}