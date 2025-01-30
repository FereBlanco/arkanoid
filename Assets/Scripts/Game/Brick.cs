using System;
using Scripts.Game;
using UnityEngine;

namespace Script.Game
{
    public class Brick : MonoBehaviour
    {
        [SerializeField, Min(50)] private int score = 50;
        [SerializeField, Min(1)] private int resistance = 1;
        [SerializeField] private PowerUpType powerType;

        public event Action<Brick> OnBrickDestroyedEvent;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Constants.TAG_BALL))
            {
                resistance--;
                if (resistance == 0)
                {
                    OnBrickDestroyedEvent?.Invoke(this);
                }
            }
        }

        internal int GetScore()
        {
            return score;
        }
    }
}