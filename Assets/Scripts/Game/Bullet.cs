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

        public void Shoot(Vector3 position)
        {
            transform.position = position;
            m_Rigidbody.velocity = m_Direction;
        }

        public void Shoot(Vector3 position, Vector3 direction)
        {
            m_Direction = 1f * direction;
            Shoot(position);
        }
    }
}
