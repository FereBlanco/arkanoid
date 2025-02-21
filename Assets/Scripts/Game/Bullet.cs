using System;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] Vector2 m_Direction = 10f * Vector2.up;
        private Rigidbody2D m_Rigidbody;
        private Collider2D m_Collider2D;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Collider2D = GetComponent<Collider2D>();
            SetInactive();
        }

        public void SetActive()
        {
            m_Collider2D.enabled = true;
            gameObject.SetActive(true);
        }

        public void SetInactive()
        {
            m_Collider2D.enabled = false;
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
