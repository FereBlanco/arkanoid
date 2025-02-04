using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{

    public class Shooter : MonoBehaviour
    {
        [Tooltip("Points from which the bullets will depart")]
        [SerializeField] Transform[] m_ShootPoints;

        [Tooltip("Bullet Pool that provides bullets to shoot")]
        [SerializeField] private BulletPool m_BulletPool;

        [Tooltip("Time between shoots")]
        [SerializeReference, Min(0.1f)] float m_CoolDownTime = 0.25f;
        private bool m_CanShoot;

        private void Awake()
        {
            Assert.IsNotNull(m_BulletPool, "ERROR: m_BulletPool not defined in Shooter.cs");
            Assert.IsNotNull(m_ShootPoints, "ERROR: m_ShootPoints not defined in Shooter.cs");

            foreach (var shootPoint in m_ShootPoints)
            {
                Assert.IsNotNull(shootPoint, "ERROR: any shootPoint of m_ShootPoints not defined in Shooter.cs");
            }
            
            m_CanShoot = true;
        }

        public void TryShoot()
        {
            if (m_CanShoot)
            {
                // Shoot
                foreach (var shootPoint in m_ShootPoints)
                {
                    Bullet bullet = m_BulletPool.GetBullet();
                    bullet.Shoot(shootPoint.position);
                }
                StartCoroutine(CoolDownCoroutine());
            }
        }

        IEnumerator CoolDownCoroutine()
        {
            m_CanShoot = false;
            // Wait until next shoot
            yield return new WaitForSeconds(m_CoolDownTime);
            m_CanShoot = true;
        }
    }
}