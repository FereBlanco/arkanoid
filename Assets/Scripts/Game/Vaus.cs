using System;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Vaus : MonoBehaviour
    {
        // Public Fields
        public float horizontalInput;

        // Private Fields
        [SerializeField, Range(10.0f, 30.0f)] private float speed = 20.0f;
        [SerializeField] private Ball ball;
        new private Rigidbody2D rigidbody;
        private Animator animator;
        private bool isFiredPressed;
        private bool isBallReleased;

        // Properties
        private VausState vausState;
        public VausState VausState
        {
            get
            {
                return vausState;
            }
            set
            {
                if (value != vausState)
                {
                    if (!vausState.Equals(VausState.Normal))
                    {
                        animator.SetTrigger(Constants.PARAMETER_NORMAL);
                    }

                    vausState = value;

                    switch (vausState)
                    {
                        case VausState.Normal:
                            // animator.SetTrigger(Constants.PARAMETER_NORMAL);
                            break;
                        case VausState.Enlarged:
                            animator.SetTrigger(Constants.PARAMETER_ENLARGED);
                            break;
                        case VausState.Laser:
                            animator.SetTrigger(Constants.PARAMETER_LASER);
                            break;
                        case VausState.Destroyed:
                            Debug.Log("DESTROYED!!!");
                            animator.SetTrigger(Constants.PARAMETER_DESTROYED);
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
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            isFiredPressed = false;
            isBallReleased = false;
            VausState = VausState.Normal;
        }

        private void Update()
        {
            // Unity recommends taking all inputs into the Update function, BUT also recommends NOT MIXING Update and FixedUpdate in the same script
            // Best solution: manage all entries in a specific script (VausInput) and all movements/actions in other script (VausMovement/VausFire)
            horizontalInput = Input.GetAxis(Constants.AXIS_HORIZONTAL);
            isFiredPressed = Input.GetAxis(Constants.AXIS_FIRE) != 0;
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = horizontalInput * speed * Vector2.right;

            if (isFiredPressed && !isBallReleased)
            {
                OnBallReleaseEvent?.Invoke();
                isBallReleased = true;
            }
        }

        // Public Methods

        // Private Methods
    }
}
