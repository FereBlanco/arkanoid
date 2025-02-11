using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.SceneManagement
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager m_Instance;
        public static LevelManager LevelManagerInstance { get => m_Instance; }

        public Button[] m_LevelButtons;

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

            foreach (var button in m_LevelButtons)
            {
                string nameScene = SceneManager.GetActiveScene().name;
                int numberScene = int.Parse(nameScene[nameScene.Length-1].ToString());
                int buttonScene = int.Parse(button.name[button.name.Length-1].ToString());
                button.gameObject.SetActive(buttonScene != numberScene);
            }
        }

        public void ChangeScene(int sceneID)
        {
            SceneManager.LoadScene("Scene0" + sceneID);
        }

        public void Exit()
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
    
