using System;
using Scripts.Game;
using UnityEngine;

namespace Script.Game
{
    public class Brick : MonoBehaviour
    {
        [SerializeField, Min(50)] private int m_Score = 50;
        [SerializeField, Min(1)] private int m_Resistance = 1;
        [SerializeField] private PowerUpType m_PowerUpType;

        public event Action<Brick> OnBrickDestroyedEvent;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Constants.TAG_BALL))
            {
                m_Resistance--;
                if (m_Resistance == 0)
                {
                    OnBrickDestroyedEvent?.Invoke(this);
                }
            }
        }

        internal int GetScore()
        {
            return m_Score;
        }

        internal bool HasPowerUp()
        {
            return m_PowerUpType != PowerUpType.None;
        }
        internal PowerUpType GetPowerUpType()
        {
            return m_PowerUpType;
        }
    }
}