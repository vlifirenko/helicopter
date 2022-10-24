using UnityEngine;

namespace Apache.View.Core
{
    public abstract class AView : MonoBehaviour
    {
        public Transform Transform
        {
            get
            {
                if (_transform == null)
                    _transform = transform;

                return transform;
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

        private Transform _transform;
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