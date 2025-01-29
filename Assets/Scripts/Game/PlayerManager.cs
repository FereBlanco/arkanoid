using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] HUDManager hUDManager;

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
                lives = Math.Min(value, Constants.MAX_LIVES);
                Debug.Log($"Lives: {lives}");
            }
        }
        private int extraLifeScoresIndex;
        private int nextExtraLifeScore;

        private void Awake()
        {
            Assert.IsNotNull(hUDManager, "ERROR: hUDManager is empty");

            Score = 0;
            Lives = 3;

            extraLifeScoresIndex = 0;
            nextExtraLifeScore = Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
        }

        internal void AddScore(int scoreToAdd)
        {
            Score += scoreToAdd;            
            CalculateExtraLifeByPoints();
        }

        private void CalculateExtraLifeByPoints()
        {
            if (Score >= nextExtraLifeScore)
            {
                Lives++;                    

                // Next extra life score is based on the constant extra life cost array
                extraLifeScoresIndex = Math.Min(extraLifeScoresIndex + 1, Constants.EXTRA_LIFE_COSTS.Length - 1);
                nextExtraLifeScore += Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
            }
        }
    }
}
