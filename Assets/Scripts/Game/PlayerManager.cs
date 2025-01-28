using System.Runtime.InteropServices;
using UnityEngine;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {
        private int score;
        private int lives;
        private int maxLives = 10;
        private int[] extraLifeScores = new int[] {200, 400, 600};
        private int extraLifeScoresIndex = 0;
        private int nextExtraLifeScore;

        private void Awake()
        {
            score = 0;
            lives = 3;
            nextExtraLifeScore = extraLifeScores[0];
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

                if (extraLifeScoresIndex < extraLifeScores.Length)
                {
                    nextExtraLifeScore = extraLifeScores[extraLifeScoresIndex];
                }
                else
                {
                    nextExtraLifeScore += extraLifeScores[extraLifeScores.Length-1];
                }
                extraLifeScoresIndex++;
                Debug.Log($"Next level points: " + nextExtraLifeScore);
            }
            return score;
        }
    }
}
