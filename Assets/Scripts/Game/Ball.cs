using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Vector2 initialVelocity = 10f * Vector2.up;
        [SerializeField] private float slowSpeed = 5f;
        new private Rigidbody2D rigidbody;
        private Vector2 initialPosition;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();

            initialPosition = transform.position;
        }

        private void Start()
        {
            Reset();
        }

        public void Release()
        {
            rigidbody.isKinematic = false;
            // rigidbody.bodyType = RigidbodyType2D.Dynamic; // other way to do this, specific for 2D
            rigidbody.velocity = initialVelocity;
        }

        public void SetSlowSpeed()
        {
            rigidbody.velocity = slowSpeed * Vector3.Normalize(rigidbody.velocity);
        }

        public void Reset()
        {
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;
            transform.position = initialPosition;
        }
    }
}
