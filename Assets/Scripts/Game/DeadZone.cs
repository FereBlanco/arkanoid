using System;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class DeadZone : MonoBehaviour
    {
        public event Action<GameObject> OnBallExitDeadZoneEvent;
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Constants.TAG_BALL))
            {
                OnBallExitDeadZoneEvent?.Invoke(other.gameObject);
            }
            if (other.CompareTag(Constants.TAG_POWER_UP) || other.CompareTag(Constants.TAG_ENEMY))
            {
                // Wise thing here: launch an event (the responsible of their destruction should be the same that the responsible of their creation)
                // Better: use an Object Pool of PowerUps
                Destroy(other.gameObject);
            }
        }

    }
}
