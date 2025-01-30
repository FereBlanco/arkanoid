using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Vector2 initialVelocity = 10f * Vector2.up;
        new private Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Release()
        {
            rigidbody.isKinematic = false;
            // rigidbody.bodyType = RigidbodyType2D.Dynamic; // other way to do this, specific for 2D
            rigidbody.velocity = initialVelocity;
        }
    }
}
