using System;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Vaus : MonoBehaviour
    {
        [SerializeField, Range(10.0f, 30.0f)] float speed = 20.0f;
        [SerializeField] Ball ball;
        public event Action OnBallReleaseEvent;
        public float horizontalInput;
        new Rigidbody2D rigidbody;
        private bool isBallReleased;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            isBallReleased = false;
        }

        private void Update()
        {
            if (Input.GetAxis(Constants.FIRE_AXIS) != 0)
            {
                OnBallReleaseEvent?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            horizontalInput = Input.GetAxis(Constants.HORIZONTAL_AXIS);
            rigidbody.velocity = horizontalInput * speed * Vector2.right;

            // if ((Input.GetAxis(Constants.FIRE_AXIS) != 0) && !isBallReleased)
            // {
            //     OnBallReleaseEvent?.Invoke();
            //     isBallReleased = true;
            // }
        }
    }
}
