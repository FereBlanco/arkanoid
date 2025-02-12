using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class HUDManager : MonoBehaviour
    {

        [SerializeField] private TMP_Text m_ScoreTMP;
        [SerializeField] private TMP_Text m_HighscoreTMP;

        private static HUDManager m_Instance;
        public static HUDManager Instance { get => m_Instance; }

        private void Awake()
        {
            // Singleton pattern
            if (null == m_Instance)
            {
                m_Instance = this; 
            }
            else
            {
                Debug.Log("HUDManager Singleton has more than one instance: this one will be destroyed");
                Destroy(gameObject); 
            }

            Assert.IsNotNull(m_ScoreTMP, "ERROR: scoreTMP is empty");
            Assert.IsNotNull(m_HighscoreTMP, "ERROR: highscoreTMP is empty");
        }

        internal void ShowScore(int currentScore)
        {
            m_ScoreTMP.text = currentScore.ToString();
        }

        internal void ShowHighscore(int currentHighcore)
        {
            m_HighscoreTMP.text = currentHighcore.ToString();
        }
    }
}