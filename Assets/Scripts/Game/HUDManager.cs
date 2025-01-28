using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreTMP;
        [SerializeField] TMP_Text highscoreTMP;

        private void Awake()
        {
            Assert.IsNotNull(scoreTMP, "ERROR: scoreTMP is empty");
            Assert.IsNotNull(highscoreTMP, "ERROR: highscoreTMP is empty");
        }

        internal void UpdateScore(int currentScore)
        {
            scoreTMP.text = currentScore.ToString();
        }
    }
}