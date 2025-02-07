using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Animator))]
    public class EnemySpawner : MonoBehaviour
    {
         [SerializeField] private Animator m_Animator;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Animator.SetTrigger(Constants.PARAMETER_TRAPDOOR_OPEN);
        }

        private void SpawmEnemyFromAnimator()
        {

        }
    }
}
