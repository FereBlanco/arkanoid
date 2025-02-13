using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
[RequireComponent(typeof(Animator), typeof(Collider2D))]
    public class Breakdoor : MonoBehaviour
    {
        Collider2D m_Collider;

        Animator m_Animator;
        private bool isOpened = false;
        public event Action OnVausEnterBreakdoorEvent;

        private void Awake()
        {
            m_Collider = GetComponent<Collider2D>();
            m_Animator = GetComponent<Animator>();
            Close();
        }

        public void Open()
        {
            if (!isOpened)
            {
                isOpened = true;
                UpdateState();
            }
        }

        public void Close()
        {
            if (isOpened)
            {
                isOpened = false;
                UpdateState();
            }
        }

        private void UpdateState()
        {
            m_Animator.SetBool(Constants.BOOL_DOOR_STATE, isOpened);
            m_Collider.isTrigger = isOpened;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.TAG_VAUS))
            {
                OnVausEnterBreakdoorEvent?.Invoke();
            }
        }

        private void Update()
        {
            // only for test purposes
            if (Input.GetKeyUp(KeyCode.U)) Open();
            if (Input.GetKeyUp(KeyCode.J)) Close();
        }

    }
}