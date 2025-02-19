using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    [RequireComponent(typeof(Animator), typeof(Shooter), typeof(Damage))]

    public class Doh : MonoBehaviour
    {
        [SerializeField] float m_MinRandomTime = 2f;
        [SerializeField] float m_MaxRandomTime = 5f;
        private Shooter m_Shooter;
        private Animator m_Animator;
        private Damage m_Damage;

        public event Action OnRoundClearEvent;

        void Awake()
        {
            Assert.IsTrue(m_MaxRandomTime > m_MinRandomTime, "ERROR: m_MinRandomTime has to be smaller than m_MaxRandomTime");
            m_Animator = GetComponent<Animator>();
            m_Shooter = GetComponent<ShooterWithTarget>();
            m_Damage = GetComponent<Damage>();
            m_Damage.OnDestroyedEvent += OnDohDestroyedCallback;
            StartCoroutine(Shoot());
        }

        private void OnDohDestroyedCallback(Damage damage)
        {
            m_Animator.SetTrigger(Constants.PARAMETER_DESTROYED);
        }

        private void DestroyedFromEditor()
        {
            OnRoundClearEvent?.Invoke();
        }

        IEnumerator Shoot()
        {
            float randomDelay = UnityEngine.Random.Range(m_MinRandomTime, m_MaxRandomTime);
            yield return new WaitForSeconds(randomDelay);
            m_Animator.SetTrigger(Constants.PARAMETER_ATTACK);
            StartCoroutine(Shoot());
        }

        private void ShootFromEditor()
        {
            m_Shooter.TryShoot();
        }
    }
}
