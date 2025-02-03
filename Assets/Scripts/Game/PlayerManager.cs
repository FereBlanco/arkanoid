using System;
using Script.Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] WorldManager worldManager;

        private HUDManager hUDManager;
        private int extraLifeScoresIndex;
        private int nextExtraLifeScore;

        private int score;
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                hUDManager.UpdateScore(score);
                CalculateExtraLifeByPoints();
            }
        }

        private int lives;
        public int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                int newValue = Math.Min(value, Constants.MAX_LIVES);

                if (0 >= newValue)
                {
                    Debug.Log("Game Over");
                }
                else
                {
                    if (newValue < lives)
                    {
                        worldManager.Reset();
                    }
                }

                lives = newValue;
                Debug.Log($"Lives: {lives}");
            }
        }

        private static PlayerManager instance;
        public static PlayerManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            Assert.IsNotNull(worldManager, "ERROR: worldManager not assigned in PlayerManager.cs");

            // Singleton pattern
            if (null == instance)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            extraLifeScoresIndex = 0;
            nextExtraLifeScore = Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
        }

        private void Start()
        {
            hUDManager = HUDManager.GetInstance();

            Lives = Constants.INITIAL_LIVES;
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
            if (Score >= nextExtraLifeScore)
            {
                AddLife();
                
                // Next extra life score is based on the constant extra life cost array
                extraLifeScoresIndex = Math.Min(extraLifeScoresIndex + 1, Constants.EXTRA_LIFE_COSTS.Length - 1);
                nextExtraLifeScore += Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
            }
        }
    }
}