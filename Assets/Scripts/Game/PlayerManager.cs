using System;
using System.Collections;
using Script.Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {
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

        private int m_ExtraLifeScoresIndex;
        private int m_NextExtraLifeScore;

        private int m_Score;
        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
                m_HUDManager.UpdateScore(m_Score);
                CalculateExtraLifeByPoints();
            }
        }

        private int m_Lives;
        public int Lives
        {
            get
            {
                return m_Lives;
            }
            set
            {
                IEnumerator WorldReset()
                {
                    // Each "yield return null;" makes the game waits for 1 frame
                    yield return null;
                    m_WorldManager.Reset();
                }

                int newValue = Math.Min(value, m_MaxLives);

                if (0 >= newValue)
                {
                    Debug.Log("Game Over");
                }
                else
                {
                    if (newValue < m_Lives)
                    {
                        StartCoroutine(WorldReset());
                    }
                }

                m_Lives = newValue;
                Debug.Log($"Lives: {m_Lives}");
            }
        }

        private static PlayerManager m_Instance;
        public static PlayerManager GetInstance()
        {
            return m_Instance;
        }

        private void Awake()
        {
            Assert.IsNotNull(m_WorldManager, "ERROR: worldManager not assigned in PlayerManager.cs");

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
            m_HUDManager = HUDManager.GetInstance();

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