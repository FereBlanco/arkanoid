using System;
using UnityEngine;

namespace Scripts.Game
{
    public class Score : MonoBehaviour
    {
        private int score;
        internal void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
        }
    }
}
