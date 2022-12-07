using UnityEngine;

namespace Apache.Model.Config
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Config/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        public WeaponItemConfig hydra;
        public WeaponItemConfig hellfire;
    }
}