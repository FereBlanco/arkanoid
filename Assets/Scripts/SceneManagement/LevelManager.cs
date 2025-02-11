using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.SceneManagement
{
    public class LevelManager : MonoBehaviour
    {
        private void Awake()
        {

        }

        public void ChangeScene(int sceneID)
        {
            SceneManager.LoadScene("Scene0" + sceneID);
        }
    }
}
    
