using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] HUDManager hUDManager;

        private int score;
        private int lives;
        private int extraLifeScoresIndex;
        private int nextExtraLifeScore;

        private void Awake()
        {
            Assert.IsNotNull(hUDManager, "ERROR: hUDManager is empty");

            score = 0;
            lives = 3;

            extraLifeScoresIndex = 0;
            nextExtraLifeScore = Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
        }

        internal void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            hUDManager.UpdateScore(scoreToAdd);
            CalculateExtraLifeByPoints();
        }

        private void CalculateExtraLifeByPoints()
        {
            if (score >= nextExtraLifeScore)
            {
                if (lives < Constants.MAX_LIVES)
                {
                    lives++;
                    Debug.Log($"Lives: {lives}");
                }

                // Next extra life score is based on the constant extra life cost array
                extraLifeScoresIndex = ((extraLifeScoresIndex < Constants.EXTRA_LIFE_COSTS.Length-1) ? extraLifeScoresIndex + 1 : Constants.EXTRA_LIFE_COSTS.Length-1);
                nextExtraLifeScore += Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
            }
        }
    }
}
