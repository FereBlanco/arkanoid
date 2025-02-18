using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class BallsManager : MonoBehaviour
    {
        [SerializeField] private Ball m_MainBall;
        List<Ball> m_Balls;
        
        private void Awake()
        {
            Assert.IsNotNull(m_MainBall, "ERROR: m_MainBall not assigned in class BallsManager");

            m_Balls = new List<Ball>() { m_MainBall };
            // Reset();
        }

        internal void Release()
        {
            foreach (var ball in m_Balls)
            {
                ball.Release();
            }
        }

        internal void Reset()
        {
            m_Balls.Clear();
            m_Balls.Add(m_MainBall);
            m_MainBall.Reset();
        }

        internal void SetSlowSpeed()
        {
            foreach (var ball in m_Balls)
            {
                ball.SetSlowSpeed();
            }
        }

        internal void Disrupt()
        {
            if (m_Balls.Count == 1)
            {
                CreateNewball(1f * Vector2.up);
                CreateNewball(1f * Vector2.down);
            }
        }

        private void CreateNewball(Vector2 offset)
        {
            Ball newBall = Instantiate(m_MainBall, m_MainBall.transform.position + (Vector3)offset, Quaternion.identity);
            // ToDo: 2 positions, up & downw, and change only direction (Profesor uses Random position)

            // PROFESOR
            // float spriteExtentsX = m_MainBall.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
            // Vector3 randomDeltaPosition = UnityEngine.Random.insideUnitCircle * spriteExtentsX;
            // newBall.transform.position = m_MainBall.transform.position + randomDeltaPosition;
            // newBall.SetDirection(UnityEngine.Random.insideUnitCircle);

            // newBall.transform.SetParent(m_MainBall.transform.parent);
            newBall.SetVelocity(m_MainBall.GetComponent<Rigidbody2D>().velocity + offset);
            m_Balls.Add(newBall);
        }

        internal void ResetBallLeft()
        {
            m_MainBall.transform.position = new Vector2(-20.0f, -10.0f);
            m_MainBall.GetComponent<Rigidbody2D>().velocity = new Vector2(-20.0f, 15.0f);
        }

        internal void ResetBallRight()
        {
            m_MainBall.transform.position = new Vector2(8.0f, -10.0f);
            m_MainBall.GetComponent<Rigidbody2D>().velocity = new Vector2(20.0f, 15.0f);
        }

        internal bool DestroyBall(Ball ball)
        {
            if (m_Balls.Count == 1)
            {
                return true;
            }
            else
            {
                m_Balls.Remove(ball);
                Destroy(ball.gameObject);
                m_MainBall = m_Balls[0];
                return false;
            }
        }
    }
}
