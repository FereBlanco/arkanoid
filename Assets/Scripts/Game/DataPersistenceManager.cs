using System;
using System.IO;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Scripts.Game
{
    public class DataPersistenceManager : MonoBehaviour
    {
        private static DataPersistenceManager m_Instance;

        private int m_Highscore = 0;
        public int Highscore
        {
            get => m_Highscore;
            set
            {
                m_Highscore = value;
                HUDManager.Instance.ShowHighscore(m_Highscore);
            }
        }

        public static DataPersistenceManager Instance { get => m_Instance; }

        [Serializable]
        class PlayerData
        {
            public int m_PlayerHighscore;

            public PlayerData(int highscore)
            {
                this.m_PlayerHighscore= highscore;
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

        private void Start()
        {
            LoadHighscore();
        }

        private void LoadHighscore()
        {
            var path = Path.Combine(Application.dataPath, FILENAME);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<PlayerData>(json);
                Highscore = data.m_PlayerHighscore;
            }
            else
            {
                Highscore = 0;
            }
        }

        internal void UpdateHighscore(int score)
        {
            if (score > Highscore)
            {
                Highscore = score;
                SaveHighscore();
            }
        }

        private void SaveHighscore()
        {
            var data = new PlayerData(Highscore);
            var json = JsonUtility.ToJson(data);
            Debug.Log(json);
            var path = Path.Combine(Application.dataPath, FILENAME);
            File.WriteAllText(path, json);
        }


        public static DataPersistenceManager GetInstance()
        {
            return m_Instance;
        }  
    }
}
