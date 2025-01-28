using System.Collections.Generic;
using Scripts.Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Game
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] Vaus vaus;
        [SerializeField] Ball ball;
        List<Brick> bricks = new List<Brick>();
        Score score;

        public void Awake()
        {
            Assert.IsNotNull(vaus, "ERROR: vaus is empty");
            Assert.IsNotNull(ball, "ERROR: ball is empty");

            SetupBricks();

            vaus.OnBallReleaseEvent += OnBallReleaseCallback;
        }

        private void SetupBricks()
        {
            bricks = new List<Brick>();
            GameObject[] brickGOs = GameObject.FindGameObjectsWithTag("Brick");
            foreach (var brickGO in brickGOs)
            {
                Brick brick = brickGO.GetComponent<Brick>();
                brick.OnBrickDestroyedEvent += OnBrickDestroyedCallback;
                bricks.Add(brick);
            }
        }

        private void OnBrickDestroyedCallback(Brick brick)
        {
            bricks.Remove(brick);
            Destroy(brick.gameObject);
            score.AddScore(brick.GetScore());
            // if (bricks.Count == 0) we reach next level
        }

        private void OnBallReleaseCallback()
        {
            ball.Release();
        }
    }
}