using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Vector2 m_InitialVelocity = 10f * Vector2.up;
        [SerializeField] private float m_SlowSpeed = 5f;
        private Rigidbody2D m_Rigidbody;
        private Vector2 m_InitialPosition;
        public Vector2 InitialPosition
        {
            get => m_InitialPosition;
            set => m_InitialPosition = value;
        }

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();

            m_InitialPosition = transform.position;
        }

        public void Release()
        {
            m_Rigidbody.isKinematic = false;
            // rigidbody.bodyType = RigidbodyType2D.Dynamic; // other way to do this, specific for 2D
            m_Rigidbody.velocity = m_InitialVelocity;
        }

        public void Reset()
        {
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.velocity = Vector2.zero;
            transform.position = m_InitialPosition;
        }

        public void SetSlowSpeed()
        {
            m_Rigidbody.velocity = m_SlowSpeed * Vector3.Normalize(m_Rigidbody.velocity);
        }

        public void SetVelocity(Vector2 newVelocity)
        {
            m_Rigidbody.velocity = newVelocity;
        }

    }
}
