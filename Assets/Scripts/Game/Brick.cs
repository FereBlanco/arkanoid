using UnityEngine;

namespace Scripts.Game
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private PowerUpType m_PowerUpType;
        public PowerUpType PowerUpType { get => m_PowerUpType; set => m_PowerUpType = value; }

        internal bool HasPowerUp()
        {
            return m_PowerUpType != PowerUpType.None;
        }
    }
}