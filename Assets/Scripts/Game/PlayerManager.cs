using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {

        private HUDManager hUDManager;

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
                lives = Math.Min(value, Constants.MAX_LIVES);
                Debug.Log($"Lives: {lives}");
            }
        }
        private int extraLifeScoresIndex;
        private int nextExtraLifeScore;

        private static PlayerManager instance;
        public static PlayerManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            // Singleton pattern
            if (null == instance)
            {
                instance = this;
            }
            else
            {
                Debug.Log("PlayerManager Singleton has more than one instance: this one will be destroyed");
                Destroy(gameObject);
            }

            extraLifeScoresIndex = 0;
            nextExtraLifeScore = Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
        }

        private void Start()
        {
            hUDManager = HUDManager.GetInstance();

            Score = 0;
            Lives = 3;
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
