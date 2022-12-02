using UnityEngine;

namespace Apache.View.Core
{
    public class AUiView : MonoBehaviour
    {
        public RectTransform Transform
        {
            get
            {
                if (_transform == null)
                    _transform = GetComponent<RectTransform>();

                return _transform;
            }
            set => _transform = value;
        }

        public GameObject GameObject
        {
            get
            {
                if (_gameObject == null)
                    _gameObject = gameObject;

                return _gameObject;
            }
            set => _gameObject = value;
        }

        private RectTransform _transform;
        private GameObject _gameObject;

        public void Show()
        {
            if (!GameObject.activeSelf)
                GameObject.SetActive(true);
        }

        public void Hide()
        {
            if (GameObject.activeSelf)
                GameObject.SetActive(false);
        }
    }
}