using Apache.View;
using Apache.View.Core;
using UnityEngine;

namespace Apache.Model
{
    public class SceneData : AView
    {
        [SerializeField] private ApacheView apache;
        [SerializeField] private Camera mainCamera;

        public ApacheView Apache => apache;
        public Camera MainCamera => mainCamera;
    }
}