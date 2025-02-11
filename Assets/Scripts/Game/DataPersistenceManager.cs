using System;
using UnityEngine;

namespace Scripts.Game
{
    // 
    public class DataPersistenceManager : MonoBehaviour
    {
        private static DataPersistenceManager m_Instance;
        private int highscore;

        public static DataPersistenceManager Instance { get => m_Instance; }

        [Serializable]
        class PlayerData
        {
            int highscore;

            public PlayerData(int highscore)
            {
                this.highscore= highscore;
            }
        }

        const string FILENAME = "Highscore.json";

        private void Awake()
        {
            // singleton pattern
            if (null == m_Instance)
            {
                m_Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        internal void UpdateScore(int score)
        {
            if (score > highscore)
            {
                highscore = score;
                // store in JSON
            }
        }

        public static DataPersistenceManager GetInstance()
        {
            return m_Instance;
        }  
    }
}
