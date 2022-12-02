using Apache.View.Core;
using UnityEngine;

namespace Apache.View.Ui
{
    public class UiTargetsContainer : AUiView
    {
        [SerializeField] private UiTarget prefab;

        public UiTarget Prefab => prefab;
    }
}