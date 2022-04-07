using UnityEngine;

namespace Mirror.Examples.RigidbodyPhysics
{
<<<<<<< HEAD
    [RequireComponent(typeof(Rigidbody))]
=======
>>>>>>> origin/alpha_merge
    public class AddForce : NetworkBehaviour
    {
        public Rigidbody rigidbody3d;
        public float force = 500f;

<<<<<<< HEAD
        void OnValidate()
        {
            rigidbody3d = GetComponent<Rigidbody>();
            rigidbody3d.isKinematic = true;
        }

        public override void OnStartServer()
        {
            rigidbody3d.isKinematic = false;
        }

        [ServerCallback]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rigidbody3d.AddForce(Vector3.up * force);
=======
        void Start()
        {
            rigidbody3d.isKinematic = !isServer;
        }

        void Update()
        {
            if (isServer && Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody3d.AddForce(Vector3.up * force);
            }
>>>>>>> origin/alpha_merge
        }
    }
}
