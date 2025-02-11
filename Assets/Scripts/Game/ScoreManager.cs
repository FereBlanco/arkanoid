using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{

    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager m_Instance;

        private int m_Score = 0;
        [SerializeField] private TMP_Text m_ScoreText;

        private int Score
        {
            get => m_Score;
            set
            {
                m_Score = value;
                ShowScore();
            }
        }

        private void Awake()
        {
            Assert.IsNotNull(m_ScoreText, "ERROR: m_ScoreText is no assigned in ScoreManager class");

            // Singleton pattern
            if (null == m_Instance)
            {
                m_Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            ShowScore();
            DontDestroyOnLoad(gameObject);
        }

        public void AddScore(int scoreToAdd)
        {
            Score += scoreToAdd;
        }

        public void ShowScore()
        {
            m_ScoreText.text = Constants.SCORE_TEXT + m_Score;
        }

        public static ScoreManager GetInstance()
        {
            return m_Instance;
        }         
    }
}
