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
        PlayerManager playerManager;

        public void Awake()
        {
            Assert.IsNotNull(vaus, "ERROR: vaus is empty");
            Assert.IsNotNull(ball, "ERROR: ball is empty");

            vaus.OnBallReleaseEvent += OnBallReleaseCallback;
            SetupBricks();
        }

        public void Start()
        {
            playerManager = PlayerManager.GetInstance().GetComponent<PlayerManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ball.transform.position = new Vector2(-14.0f, 7.0f);
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(20.0f, 15.0f);
            }
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
            playerManager.AddScore(brick.GetScore());
            bricks.Remove(brick);
            brick.OnBrickDestroyedEvent -= OnBrickDestroyedCallback;
            Destroy(brick.gameObject);
            // if (bricks.Count == 0) we reach next level
        }

        private void OnBallReleaseCallback()
        {
            ball.Release();
        }
    }
}