using System;
using Scripts.Game;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Game
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
