using System.Collections.Generic;
using Scripts.Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Game
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] Vaus vaus;
        [SerializeField] Ball ball;
        [SerializeField] HUDManager hUDManager;
        List<Brick> bricks = new List<Brick>();
        PlayerManager playerManager;

        public void Awake()
        {
            Assert.IsNotNull(vaus, "ERROR: vaus is empty");
            Assert.IsNotNull(ball, "ERROR: ball is empty");
            Assert.IsNotNull(hUDManager, "ERROR: hUDManager is empty");

            SetupBricks();
            playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

            vaus.OnBallReleaseEvent += OnBallReleaseCallback;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ball.transform.position = new Vector2(6.0f, 5.0f);
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
            bricks.Remove(brick);
            Destroy(brick.gameObject);
            var currentScore = playerManager.AddScore(brick.GetScore());
            hUDManager.UpdateScore(currentScore);
            // if (bricks.Count == 0) we reach next level
        }

        private void OnBallReleaseCallback()
        {
            ball.Release();
        }
    }
}