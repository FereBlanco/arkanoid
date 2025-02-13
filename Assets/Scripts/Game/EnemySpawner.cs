using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Animator))]
    public class EnemySpawner : MonoBehaviour
    {
         [SerializeField] private Trapdoor[] m_Trapdoors;
         [SerializeField] private int m_MaxEnemies;
         [SerializeField] float m_TimeBetweenEnemies;
         [SerializeField] Enemy[] m_EnemyPrefabs;
         private List<Enemy> m_CurrentEnemies;
         private WaitForSeconds m_WaitBetweenEnemies;
         private bool m_IsCreatingEnemies = false;

        private void Awake()
        {
            m_CurrentEnemies = new List<Enemy>();
            m_WaitBetweenEnemies = new WaitForSeconds(m_TimeBetweenEnemies);
            StartCreateEnemies();
        }

        public void StartCreateEnemies()
        {
            m_IsCreatingEnemies = true;
            StartCoroutine(CreateEnemies());
        }

        public void StopCreateEnemies()
        {
            m_IsCreatingEnemies = false;
            StopCoroutine(CreateEnemies());
        }

        IEnumerator CreateEnemies()
        {
            yield return m_WaitBetweenEnemies;

            if (m_IsCreatingEnemies && m_CurrentEnemies.Count < m_MaxEnemies)
            {
                int numberTrapdoor = Random.Range(0, 2);
                int numberEnemy = Random.Range(0, m_EnemyPrefabs.Length);

                m_Trapdoors[numberTrapdoor].Open();
                Enemy newEnemy = Instantiate(m_EnemyPrefabs[numberEnemy], m_Trapdoors[numberTrapdoor].transform.position + 4f * Vector3.up, Quaternion.identity);
                m_CurrentEnemies.Add(newEnemy);
                yield return new WaitForSeconds(4f);
                m_Trapdoors[numberTrapdoor].Close();
            }

            StartCoroutine(CreateEnemies());
        }

        internal void Reset()
        {
            foreach (var enemy in m_CurrentEnemies)
            {
                Destroy(enemy.gameObject);
            }   
            m_CurrentEnemies.Clear();
        }
    }

}
