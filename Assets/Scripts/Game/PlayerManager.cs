using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {
        private static PlayerManager m_Instance;
        public static PlayerManager Instance { get => m_Instance; }

        [Header("Managers")]

        [Tooltip("World Manager that manages all the game")]  
        [SerializeField] WorldManager m_WorldManager;

        [Tooltip("HUD Manager that manages the Head-Up Display")]  
        [SerializeField] private HUDManager m_HUDManager;
        [Header("Player LIVES")]

        [Tooltip("Initial lives number")]
        [SerializeField] int m_InitialLives;

        [Tooltip("Maximum lives number")]
        [SerializeField] int m_MaxLives;

        [Tooltip("Extra lives points costs")]
        [SerializeField] int[] m_ExtraLifeCosts;

        [Header("Data")]
        [SerializeField] private DataPersistenceManager m_dataPersistanceManager;

        private int m_ExtraLifeScoresIndex;
        private int m_NextExtraLifeScore;

        private int m_Score;
        public int Score
        {
            get => m_Score;
            set
            {
                m_Score = value;
                m_HUDManager.ShowScore(m_Score);
                // HUDManager.Instance.ShowScore(m_Score);
                m_dataPersistanceManager.UpdateHighscore(m_Score);
                CalculateExtraLifeByPoints();
            }
        }

        private int m_Lives;
        public int Lives
        {
            get => m_Lives;
            set
            {
                IEnumerator WorldReset()
                {
                    // each "yield return null;" makes the game waits for 1 frame
                    yield return null;
                    m_WorldManager.Reset();
                }

                if (value <= m_MaxLives)
                {
                    if (value <= 0)
                    {
                        Debug.Log("Game Over");
                        m_dataPersistanceManager.UpdateHighscore(m_Score);
                    }
                    else
                    {
                        if (value < m_Lives)
                        {
                            StartCoroutine(WorldReset());
                        }
                        m_Lives = value;
                    }
                }
            }
        }

        private void Awake()
        {
            Assert.IsNotNull(m_WorldManager, "ERROR: m_WorldManager not assigned in class PlayerManager");

            // Singleton pattern
            if (null == m_Instance)
            {
                m_Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            m_ExtraLifeScoresIndex = 0;
            m_NextExtraLifeScore = m_ExtraLifeCosts[m_ExtraLifeScoresIndex];
        }

        private void Start()
        {
            m_HUDManager = HUDManager.Instance;
            m_dataPersistanceManager = DataPersistenceManager.Instance;

            Lives = m_InitialLives;
            Score = 0;
        }

        internal void AddScore(int scoreToAdd)
        {
            Score += scoreToAdd;            
        }

        public void AddLife()
        {
            Lives++;                    
        }

        private void CalculateExtraLifeByPoints()
        {
            if (Score >= m_NextExtraLifeScore)
            {
                AddLife();
                
                // Next extra life score is based on the constant extra life cost array
                m_ExtraLifeScoresIndex = Math.Min(m_ExtraLifeScoresIndex + 1, m_ExtraLifeCosts.Length - 1);
                m_NextExtraLifeScore += m_ExtraLifeCosts[m_ExtraLifeScoresIndex];
            }
        }
    }
}