using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
        private static T m_Instance;
        public static T Instance { get => m_Instance; }

        private void Awake()
        {
            if (null == m_Instance)
            {
                GameObject singleton = new GameObject();
                m_Instance = singleton.AddComponent<T>();
            }
            else
            {
                // there is more than one instance: this one will be destroyed
                Destroy(gameObject); 
            }
        }
}