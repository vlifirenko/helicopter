using UnityEngine;

namespace Apache.Model.Config
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Config/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public float moveSpeed = 2f;
        public float rotationSpeed = 2f;
    }
}