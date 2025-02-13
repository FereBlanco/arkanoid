using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager m_Instance;
        public static LevelManager Instance { get => m_Instance; }

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

            // DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                Exit();
            }   
        }

        internal void RoundClear()
        {
            Debug.Log("ROUND CLEAR");
        }

        public void Exit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        // IEnumerator LoadScene(string sceneName)
        // {
        //     // yield return null;
        //     AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //     while (!asyncOperation.isDone)
        //     {
        //         yield return null;
        //     }

        //     UpdateButtons();
        // }
    }
}
    
