using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    [RequireComponent(typeof(Animator), typeof(Shooter), typeof(Damage))]

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
            // Assert.IsTrue(m_MaxRandomTime > m_MinRandomTime, "ERROR: m_MinRandomTime has to be smaller than m_MaxRandomTime");
            m_Animator = GetComponent<Animator>();
            m_Shooter = GetComponent<ShooterWithTarget>();
            m_Damage = GetComponent<Damage>();
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

            if (m_CurrentDohState < m_DohStates.Length - 1)
            {
                m_CurrentDohState++;
                SetValues(m_CurrentDohState);
            }
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
