using System.Runtime.InteropServices;
using UnityEngine;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {
        private int score;
        private int lives;
        private int maxLives = 10;
        private int extraLifeScoresIndex;
        private int nextExtraLifeScore;

        private void Awake()
        {
            score = 0;
            lives = 3;

            extraLifeScoresIndex = 0;
            nextExtraLifeScore = Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
        }

        internal int AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            if (score >= nextExtraLifeScore)
            {
                if (lives < maxLives)
                {
                    lives++;
                    Debug.Log($"Lives: {lives}");
                }

                // Calculation of the next extra life score based on the constant extra life cost array
                extraLifeScoresIndex = ((extraLifeScoresIndex < Constants.EXTRA_LIFE_COSTS.Length-1) ? extraLifeScoresIndex + 1 : Constants.EXTRA_LIFE_COSTS.Length-1);
                nextExtraLifeScore += Constants.EXTRA_LIFE_COSTS[extraLifeScoresIndex];
            }
            return score;
        }
    }
}
