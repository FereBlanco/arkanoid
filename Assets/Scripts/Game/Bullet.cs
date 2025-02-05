using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] Vector2 m_Direction = Vector2.up * 10f;
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
    }
}
