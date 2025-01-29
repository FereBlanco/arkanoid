using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class HUDManager : MonoBehaviour
    {

        [SerializeField] TMP_Text scoreTMP;
        [SerializeField] TMP_Text highscoreTMP;

        private static HUDManager instance;
        public static HUDManager GetInstance()
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
                Debug.Log("HUDManager Singleton has more than one instance: this one will be destroyed");
                Destroy(gameObject); 
            }

            Assert.IsNotNull(scoreTMP, "ERROR: scoreTMP is empty");
            Assert.IsNotNull(highscoreTMP, "ERROR: highscoreTMP is empty");
        }


        internal void UpdateScore(int currentScore)
        {
            scoreTMP.text = currentScore.ToString();
        }
    }
}