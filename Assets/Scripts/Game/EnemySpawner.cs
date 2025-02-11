using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Animator))]
    public class EnemySpawner : MonoBehaviour
    {
         [SerializeField] private Trapdoor[] m_Trapdoors;

        private void Awake()
        {
            m_Trapdoors[0].Open();
            m_Trapdoors[1].Open();
        }
    }
}
