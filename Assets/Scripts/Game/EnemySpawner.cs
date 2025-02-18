using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class EnemySpawner : MonoBehaviour
    {
         [SerializeField] private Trapdoor[] m_Trapdoors;
         [SerializeField] private int m_MaxEnemies;
         [SerializeField] float m_TimeBetweenEnemies;
         EnemyPool m_PoolEnemies;
         private List<Enemy> m_CurrentEnemies;
         private WaitForSeconds m_WaitBetweenEnemies;
         private bool m_IsCreatingEnemies = false;

        private void Awake()
        {
            m_PoolEnemies = GetComponentInChildren<EnemyPool>();
            Assert.IsNotNull(m_PoolEnemies, "ERROR: m_PoolEnemies not found in class WorldManager children");

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

                m_Trapdoors[numberTrapdoor].Open();
                
                Enemy newEnemy = m_PoolEnemies.GetEnemy();
                newEnemy.transform.position = m_Trapdoors[numberTrapdoor].transform.position + 3f * Vector3.up;
                newEnemy.transform.rotation = Quaternion.identity;
                newEnemy.Activate();
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
                enemy.Release();
            }   
            m_CurrentEnemies.Clear();
        }
    }

}
