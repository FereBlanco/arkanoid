using System;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private float m_Speed = 200.0f;
        private Rigidbody2D m_RigidBody;

        [SerializeField] private PowerUpType m_PowerUpType;
        public PowerUpType PowerUpType {
            get
            {
                return m_PowerUpType;
            }
            set
            {
                m_PowerUpType = value;
            }
        }

        public event Action<PowerUp> OnPowerUpActivateEvent;

        private void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody2D>();
            m_RigidBody.velocity = m_Speed * Time.deltaTime * Vector2.down;

            m_PowerUpType = PowerUpType.None;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.TAG_VAUS))
            {
                OnPowerUpActivateEvent?.Invoke(this);
            }
        }
    }
}