using System;
using UnityEngine;

namespace Scripts.Game
{
    public enum PowerUpType
    {
        None, Laser, Enlarge, Catch, Slow, Break, Disruption, Player
    }

    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private PowerUpType powerUpType;
        [SerializeField] private float speed = 200.0f;
        private Rigidbody2D rg;

        public event Action<PowerUp> OnPowerUpActivateEvent;

        private void Awake()
        {
            rg = GetComponent<Rigidbody2D>();
            rg.velocity = speed * Time.deltaTime * Vector2.down;
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