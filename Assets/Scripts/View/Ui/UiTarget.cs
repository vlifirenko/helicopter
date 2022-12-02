using Apache.View.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Apache.View.Ui
{
    public class UiTarget : AUiView
    {
        [SerializeField] private Image cross;
        [SerializeField] private TMP_Text distance;

        public Image Cross => cross;
        public TMP_Text Distance => distance;
    }
}