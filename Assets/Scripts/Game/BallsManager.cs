using System;
using System.Collections.Generic;
using Scripts.Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Game
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
            Debug.Log("Release");
            foreach (var ball in m_Balls)
            {
                ball.Release();
            }
        }

        internal void Reset()
        {
            Debug.Log("Reset");
            m_Balls.Clear();
            m_Balls.Add(m_MainBall);
            m_MainBall.Reset();
        }

        internal void UpdateMainBall()
        {
            if (m_Balls.Count > 0)
            {
                m_MainBall = m_Balls[0];
            }
        }

        internal void SetSlowSpeed()
        {
            foreach (var ball in m_Balls)
            {
                ball.SetSlowSpeed();
            }
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
    }
}
