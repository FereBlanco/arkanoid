using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] Vector2 m_Direction = 10f * Vector2.up;
        private Rigidbody2D m_Rigidbody;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            m_Rigidbody.velocity = m_Direction;
        }

        private void OnDisable()
        {
            m_Rigidbody.velocity = Vector2.zero;
        }

        public void Shoot(Vector3 position)
        {
            transform.position = position;
        }

        public void Shoot(Vector3 position, Vector3 direction)
        {
            Shoot(position);
            m_Direction = 5f * direction;
        }
    }
}
