using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class ShooterWithTarget : Shooter
    {
        [SerializeField] Transform m_TargetTransform;
        protected override void Awake()
        {
            base.Awake();

            Assert.IsNotNull(m_TargetTransform, "ERROR: m_TargetTransform is not assiged in ShooterWithTarget");
        }

        protected override void Shoot(Bullet bullet, Vector3 origin)
        {
            Vector3 direction = m_TargetTransform.position - transform.position;
            bullet.Shoot(origin, direction);
        }
    }
}