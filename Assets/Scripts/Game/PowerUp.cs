using System;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private float speed = 200.0f;
        private Rigidbody2D rg;

        [SerializeField] private PowerUpType powerUpType;
        public PowerUpType PowerUpType { get; set; }

        public event Action<PowerUp> OnPowerUpActivateEvent;

        private void Awake()
        {
            rg = GetComponent<Rigidbody2D>();
            rg.velocity = speed * Time.deltaTime * Vector2.down;

            powerUpType = PowerUpType.None;
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