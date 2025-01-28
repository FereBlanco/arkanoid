using UnityEngine;

namespace Scripts.Game
{
    public class PlayerManager : MonoBehaviour
    {
        private int score;

        private void Awake()
        {
            score = 0;
        }

        internal int AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            return score;
        }
    }
}
