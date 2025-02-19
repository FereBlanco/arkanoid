using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    [RequireComponent(typeof(Doh), typeof(Damage))]
    public class WorldManagerWithDoh : WorldManager
    {
        private Doh m_Doh;
        private Damage m_DohDamage;

        protected override void Awake()
        {
            base.Awake();

            m_Doh = GetComponentInChildren<Doh>();
            m_DohDamage = GetComponentInChildren<Damage>();
            Assert.IsNotNull(m_DohDamage, "ERROR: m_DohDamage not assiged in class Doh");

            m_DohDamage.OnDestroyedEvent += OnDohDestroyedCallback;
            m_Doh.OnRoundClearEvent += OnRoundClearCallback;
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