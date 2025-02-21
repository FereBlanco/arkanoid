using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class Doh : MonoBehaviour
    {
        [Serializable]
        class DohState
        {
            [SerializeField] private float m_DelayBetweenBurstsTime;
            public float DelayBetweenBurstsTime { get => m_DelayBetweenBurstsTime; }
            [SerializeField] private float m_DelayBetweenBulletsTime;
            public float DelayBetweenBulletsTime { get => m_DelayBetweenBulletsTime; }
            [SerializeField] private int m_NumberOfBullets;
            public int NumberOfBullets { get => m_NumberOfBullets; }
            [SerializeField] private AnimatorOverrideController m_AnimatorOverrideController;
            public AnimatorOverrideController mAnimatorOverrideController { get => m_AnimatorOverrideController; }
        }
        [SerializeField] private DohState[] m_DohStates;
        private int m_CurrentDohState = 0;

        private WaitForSeconds m_DelayBetweenBursts;
        private WaitForSeconds m_DelayBetweenBullets;
        private Shooter m_Shooter;
        private Animator m_Animator;
        private Damage m_Damage;
        private int m_NumberOfBullets;

        public event Action OnRoundClearEvent;

        void Awake()
        {
            m_Animator = GetComponent<Animator>();
            Assert.IsNotNull(m_Animator, "ERROR: m_Animator not set in Doh class");

            m_Shooter = GetComponent<ShooterWithTarget>();
            Assert.IsNotNull(m_Shooter, "ERROR: m_Shooter not set in Doh class");

            m_Damage = GetComponent<Damage>();
            Assert.IsNotNull(m_Damage, "ERROR: m_Damage not set in Doh class");

            m_Damage.OnDestroyedEvent += OnDohDestroyedCallback;

            SetValues(m_CurrentDohState);
        }

        private void SetValues(int stateNumber)
        {
            Debug.Log($"State: {stateNumber}");
            m_DelayBetweenBursts = new WaitForSeconds(m_DohStates[stateNumber].DelayBetweenBurstsTime);
            m_DelayBetweenBullets = new WaitForSeconds(m_DohStates[stateNumber].DelayBetweenBulletsTime);
            m_NumberOfBullets = m_DohStates[stateNumber].NumberOfBullets;
        }

        private void Start()
        {
            StartCoroutine(RandomShootRoutine());
        }

        private void OnDamageReceivedCallback()
        {
            int resistance = m_Damage.GetRersistance();
            UpdateState(resistance);
        }

        private void UpdateState(int resistance)
        {
        //     if (resistance <= m_DohStates[m_CurrentDohState].minStateValue)
        //     {
        //         m_CurrentDohState++;
        //     }
        }

        private void OnDohDestroyedCallback(Damage damage)
        {
            m_Animator.SetTrigger(Constants.PARAMETER_DESTROYED);
        }

        private void DestroyedFromAnimation()
        {
            OnRoundClearEvent?.Invoke();
        }

        IEnumerator RandomShootRoutine()
        {
            yield return m_DelayBetweenBursts;
            m_Animator.SetTrigger(Constants.PARAMETER_ATTACK);

            StartCoroutine(RandomShootRoutine());

            // if (m_CurrentDohState < m_DohStates.Length - 1)
            // {
            //     m_CurrentDohState++;
            //     SetValues(m_CurrentDohState);
            // }
        }

        public void ShootFromAnimation()
        {
            StartCoroutine(TryBurstShoot());
        }

        IEnumerator TryBurstShoot()
        {
            for (int i = 0; i < m_NumberOfBullets; i++)
            {
                m_Shooter.TryShoot();
                yield return m_DelayBetweenBullets;
            }
        }
    }
}
