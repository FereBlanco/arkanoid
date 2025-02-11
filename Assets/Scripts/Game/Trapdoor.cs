using UnityEngine;

namespace Scripts.Game
{
[RequireComponent(typeof(Animator))]
    public class Trapdoor : MonoBehaviour
    {
        Animator m_Animator;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            Open();
        }

        public void Open()
        {
            m_Animator.SetTrigger(Constants.PARAMETER_TRAPDOOR_OPEN);
        }

        public void Close()
        {
            m_Animator.SetTrigger(Constants.PARAMETER_TRAPDOOR_CLOSE);
        }
    }
}