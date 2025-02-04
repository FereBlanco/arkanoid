using System;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Vaus : MonoBehaviour
    {
        // Public Fields
        public float m_HorizontalInput;

        // Private Fields
        [SerializeField, Range(10.0f, 30.0f)] private float m_Speed = 20.0f;
        [SerializeField] private Ball m_Ball;
        private Rigidbody2D m_Rigidbody;
        private Animator m_Animator;
        private bool m_IsFiredPressed;
        private bool m_IsBallReleased;
        private Vector2 m_InitialPosition;

        // Properties
        private VausState m_VausState;
        public VausState VausState
        {
            get
            {
                return m_VausState;
            }
            set
            {
                if (value != m_VausState)
                {
                    if (!m_VausState.Equals(VausState.Normal))
                    {
                        m_Animator.SetTrigger(Constants.PARAMETER_NORMAL);
                    }

                    m_VausState = value;

                    switch (m_VausState)
                    {
                        case VausState.Normal:
                            // animator.SetTrigger(Constants.PARAMETER_NORMAL);
                            break;
                        case VausState.Enlarged:
                            m_Animator.SetTrigger(Constants.PARAMETER_ENLARGED);
                            break;
                        case VausState.Laser:
                            m_Animator.SetTrigger(Constants.PARAMETER_LASER);
                            break;
                        case VausState.Destroyed:
                            m_Animator.SetTrigger(Constants.PARAMETER_DESTROYED);
                            break;
                    }                    
                }
            }
        }

        // Events / Delegates
        public event Action OnBallReleaseEvent;

        // Monobehaviour Methods
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();

            m_InitialPosition = transform.position;
        }

        private void Start()
        {
            Reset();
        }

        private void Update()
        {
            // Unity recommends taking all inputs into the Update function, BUT also recommends NOT MIXING Update and FixedUpdate in the same script
            // Best solution: manage all entries in a specific script (VausInput) and all movements/actions in other script (VausMovement/VausFire)
            m_HorizontalInput = Input.GetAxis(Constants.AXIS_HORIZONTAL);
            m_IsFiredPressed = Input.GetAxis(Constants.AXIS_FIRE) != 0;
        }

        private void FixedUpdate()
        {
            m_Rigidbody.velocity = m_HorizontalInput * m_Speed * Vector2.right;

            if (m_IsFiredPressed && !m_IsBallReleased)
            {
                OnBallReleaseEvent?.Invoke();
                m_IsBallReleased = true;
            }
        }

        // Public Methods
        internal void Reset()
        {
            transform.position = m_InitialPosition;
            m_IsFiredPressed = false;
            m_IsBallReleased = false;
            VausState = VausState.Normal;
        }        

        // Private Methods
    }
}
