using UnityEngine;

namespace Apache.Model.Config
{
    [CreateAssetMenu(fileName = "Global", menuName = "Config/Global Config")]
    public class GlobalConfig : ScriptableObject
    {
        public LayerMask groundLayer;
        public LayerMask enemyLayer;
        public bool isGamepad;
    }
}