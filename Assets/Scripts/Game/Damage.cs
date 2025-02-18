using System;
using UnityEngine;

namespace Scripts.Game
{
    public class Damage : MonoBehaviour
    {
        [SerializeField, Min(50)] private int m_Score = 50;
        [SerializeField, Min(1)] private int m_Resistance = 1;
        
        public event Action<Damage> OnDestroyedEvent;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Constants.TAG_BALL))
            {
                UpdateResistance();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Constants.TAG_BULLET))
            {
                UpdateResistance();
                other.gameObject.SetActive(false);
            }
        }

        private void UpdateResistance()
        {
            m_Resistance--;
            if (m_Resistance == 0)
            {
                OnDestroyedEvent?.Invoke(this);
            }
        }

        internal int GetScore()
        {
            return m_Score;
        }
    }
}
