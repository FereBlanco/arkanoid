using UnityEngine;

namespace Scripts.Game
{
[RequireComponent(typeof(Animator))]
    public class Trapdoor : MonoBehaviour
    {
        Animator m_Animator;
        private bool isOpened = false;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            isOpened = false;
        }

        public void Open()
        {
            isOpened = true;
            m_Animator.SetBool(Constants.BOOL_DOOR_STATE, isOpened);
        }

        public void Close()
        {
            isOpened = false;
            m_Animator.SetBool(Constants.BOOL_DOOR_STATE, isOpened);
        }
    }
}