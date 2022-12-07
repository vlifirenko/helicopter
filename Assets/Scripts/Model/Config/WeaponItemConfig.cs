using System;
using Apache.View;

namespace Apache.Model.Config
{
    [Serializable]
    public class WeaponItemConfig
    {
        public AProjectile prefab;
        public float distance;
        public float damage;
        public float speed;
        public float amount;
    }
}