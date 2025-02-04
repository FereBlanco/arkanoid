
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] Bullet m_BulletPrefab;
        [SerializeField, Min(1)] int m_PoolSize = 40;

        Bullet[] m_Bullets;
        int m_CurrentIndex = 0;

        private void Awake()
        {
            Assert.IsNotNull(m_BulletPrefab, "ERROR: m_BulletPrefab is not asigned in BulletPool.cs");

            CreateBullets();
        }

        private void CreateBullets()
        {
            m_Bullets = new Bullet[m_PoolSize];

            for (int i = 0; i < m_PoolSize; i++)
            {
                Bullet newBullet = Instantiate(m_BulletPrefab);
                newBullet.name = "Bullet" + i;
                newBullet.transform.parent = this.transform;
                newBullet.gameObject.SetActive(false);
                m_Bullets[i] = newBullet;
            }
        }

        public Bullet GetBullet()
        {
            Bullet bullet = m_Bullets[m_CurrentIndex];
            bullet.gameObject.SetActive(true);

            m_CurrentIndex++;
            if (m_CurrentIndex == m_PoolSize)
            {
                m_CurrentIndex = 0;
            }
            
            return bullet;
        }
    }
}