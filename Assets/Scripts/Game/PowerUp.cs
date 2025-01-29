using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Scripts.Game
{
    public enum PowerUpType
    {
        Enlarge, Catch, Laser, Disruption, Player, Break, Slow
    }

    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] PowerUpType powerUpType;
        [SerializeField] float speed = 200.0f;
        Rigidbody2D rg;

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