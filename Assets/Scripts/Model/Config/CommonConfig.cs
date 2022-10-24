using UnityEngine;

namespace Apache.Model.Config
{
    [CreateAssetMenu(fileName = "Common", menuName = "Config/Common Config")]
    public class CommonConfig : ScriptableObject
    {
        public LayerMask groundLayer;
    }
}