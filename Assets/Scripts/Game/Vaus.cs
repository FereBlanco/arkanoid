using System;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Vaus : MonoBehaviour
    {
        // Public Fields
        public float horizontalInput;

        // Private Fields
        [SerializeField, Range(10.0f, 30.0f)] private float speed = 20.0f;
        [SerializeField] private Ball ball;
        new private Rigidbody2D rigidbody;
        private bool isFiredPressed = false;
        private bool isBallReleased = false;

        // Events / Delegates
        public event Action OnBallReleaseEvent;

        // Monobehaviour Methods
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Unity recommends taking all inputs into the Update function, BUT also recommends NOT MIXING Update and FixedUpdate in the same script
            // Possible solution: manage all entries in a specific script, not here in the Vaus script
            horizontalInput = Input.GetAxis(Constants.HORIZONTAL_AXIS);
            isFiredPressed = Input.GetAxis(Constants.FIRE_AXIS) != 0;
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
