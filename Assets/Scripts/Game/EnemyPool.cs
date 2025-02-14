
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] Enemy[] m_EnemiesPrefab;
        int m_PoolSize = 40;

        private List<Enemy> m_AvailableEnemies;

        private void Awake()
        {
            Assert.IsNotNull(m_EnemiesPrefab, "ERROR: m_BulletPrefab is not asigned in BulletPool.cs");

            m_PoolSize = m_EnemiesPrefab.Length;
            CreateEnemies();
        }

        private void CreateEnemies()
        {
            m_AvailableEnemies = new List<Enemy>();

            for (int i = 0; i < m_PoolSize; i++)
            {
                Enemy newEnemy = Instantiate(m_EnemiesPrefab[i]);
                newEnemy.name = "Bullet" + i;
                newEnemy.Pool = this;
                newEnemy.transform.parent = this.transform;
                newEnemy.gameObject.SetActive(false);
                m_AvailableEnemies.Add(newEnemy);
            }
        }

        public Enemy GetEnemy()
        {
            if (m_AvailableEnemies.Count > 0)
            {
                int randomEnemy = Random.Range(0, m_AvailableEnemies.Count);
                Enemy enemy = m_AvailableEnemies[randomEnemy];
                enemy.gameObject.SetActive(true);
                return enemy;
            }
            else
            {
                return null;
            }
        }

        public void ReturnToPool(Enemy enemy)
        {
            m_AvailableEnemies.Add(enemy);
            enemy.gameObject.SetActive(false);
        }        
    }
}