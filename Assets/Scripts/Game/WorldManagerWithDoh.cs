using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class WorldManagerWithDoh : WorldManager
    {
        private Doh m_Doh;
        private Damage m_DohDamage;

        protected override void Awake()
        {
            base.Awake();

            m_Doh = GetComponentInChildren<Doh>();
            m_DohDamage = GetComponentInChildren<Damage>();
            Assert.IsNotNull(m_Doh, "ERROR: m_Doh not assigned in class WorldManagerWithDoh children");
            Assert.IsNotNull(m_DohDamage, "ERROR: m_DohDamage not assigned in class WorldManagerWithDoh children");

            m_DohDamage.OnDestroyedEvent += OnDohDestroyedCallback;
            m_DohDamage.OnDamageReceivedEvent += OnDamageReceivedCallback;
            m_Doh.OnRoundClearEvent += OnRoundClearCallback;
        }

        private void OnDamageReceivedCallback(Damage damage)
        {
            Debug.Log("Damage received");
            UpdateDamage();
        }

        private void UpdateDamage()
        {
            Debug.Log("Update damage");
        }

        private void OnDohDestroyedCallback(Damage damage)
        {
            m_DohDamage.OnDestroyedEvent -= OnDohDestroyedCallback;
            m_PlayerManager.AddScore(damage.GetScore());
        }
        public void OnRoundClearCallback()
        {
            Debug.Log("GAME FINISHED");
            LevelManager.Instance.RoundClear();
        }
    }
}