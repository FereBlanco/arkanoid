using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class WorldManagerWithDoh : WorldManager
    {
        private Damage m_DohDamage;

        protected override void Awake()
        {
            base.Awake();

            m_DohDamage = GetComponentInChildren<Damage>();
            Assert.IsNotNull(m_DohDamage, "ERROR: m_DohDamage not assiged in class Doh");

            m_DohDamage.OnDestroyedEvent += OnDohDestroyedCallback;
        }

        private void OnDohDestroyedCallback(Damage damage)
        {
            m_DohDamage.OnDestroyedEvent -= OnDohDestroyedCallback;
            m_PlayerManager.AddScore(damage.GetScore());
        }
    }
}