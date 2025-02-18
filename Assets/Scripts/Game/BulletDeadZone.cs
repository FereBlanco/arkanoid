using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class BulletDeadZone : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Constants.TAG_BULLET))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
