using System;
using UnityEngine;

namespace Script.Game
{
    public class Brick : MonoBehaviour
    {
        [SerializeField, Min(50)] private int score = 50;
        [SerializeField, Min(1)] private int resistance = 1;

        public event Action<Brick> OnBrickDestroyedEvent;

        internal int GetScore()
        {
            return score;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                resistance--;
                if (resistance == 0)
                {
                    OnBrickDestroyedEvent?.Invoke(this);
                }
            }
        }
    }
}