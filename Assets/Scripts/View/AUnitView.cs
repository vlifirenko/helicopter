using Apache.Model.Config;
using Apache.View.Core;
using UnityEngine;

namespace Apache.View
{
    public abstract class AUnitView : AView
    {
        [SerializeField] private UnitConfig config;
        [SerializeField] private Rigidbody rootRigidbody;
        [SerializeField] private Collider rootCollider;

        public Rigidbody Rigidbody => rootRigidbody;
        public Collider Collider => rootCollider;
        public UnitConfig Config => config;
    }
}