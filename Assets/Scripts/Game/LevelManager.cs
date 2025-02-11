using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Game
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager m_Instance;

        public Button[] m_LevelButtons;

        private bool m_FirstLoad = true;

        private void Awake()
        {
            // Singleton pattern
            if (null == m_Instance)
            {
                m_Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            ChangeScene(1);
            m_FirstLoad = false;
        }

        public void ChangeScene(int sceneID)
        {
            if (true != m_FirstLoad) ScoreManager.GetInstance().AddScore(5);
            StartCoroutine(LoadScene("Scene0" + sceneID));
        }

        public void Exit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        IEnumerator LoadScene(string sceneName)
        {
            // yield return null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            for (int i = 0; i < m_LevelButtons.Length; i++)
            {
                string nameScene = SceneManager.GetActiveScene().name;
                string numberSceneString = nameScene[nameScene.Length - 1].ToString();
                int numberScene = int.Parse(numberSceneString);
                int buttonScene = int.Parse(m_LevelButtons[i].name[m_LevelButtons[i].name.Length - 1].ToString());
                m_LevelButtons[i].gameObject.SetActive(buttonScene != numberScene);
            }
        }

        public static LevelManager GetInstance()
        {
            return m_Instance;
        }        
    }
}
    
