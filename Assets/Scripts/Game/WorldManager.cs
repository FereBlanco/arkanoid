using System.Collections.Generic;
using Scripts.Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Game
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private Vaus vaus;
        [SerializeField] private Ball ball;
        [SerializeField] private PowerUp[] powerUpPrefabs;
        private List<Brick> bricks = new List<Brick>();
        private PlayerManager playerManager;

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
            // Only to test purposes
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ball.transform.position = new Vector2(-20.0f, -10.0f);
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-20.0f, 15.0f);
            }
            // Only to test purposes
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                ball.transform.position = new Vector2(8.0f, -10.0f);
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(20.0f, 15.0f);
            }
            // Only to test purposes
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("K key");
                vaus.VausState = VausState.Destroyed;
            }
        }

        private void OnBrickDestroyedCallback(Brick brick)
        {
            playerManager.AddScore(brick.GetScore());
            bricks.Remove(brick);
            brick.OnBrickDestroyedEvent -= OnBrickDestroyedCallback;

            if (brick.HasPowerUp())
            {
                PowerUpType powerUpType = brick.GetPowerUpType();
                PowerUp newPowerUp = Instantiate(powerUpPrefabs[(int)powerUpType], brick.transform.position, Quaternion.identity);
                newPowerUp.PowerUpType = powerUpType;
                newPowerUp.OnPowerUpActivateEvent += OnPowerUpActivateCallBack;
            }
            Destroy(brick.gameObject);
            // if (bricks.Count == 0) we reach next level
        }

        private void OnBallReleaseCallback()
        {
            ball.Release();
        }

        private void OnPowerUpActivateCallBack(PowerUp powerUp)
        {
            switch (powerUp.PowerUpType)
            {
                case PowerUpType.Break:
                    break;
                case PowerUpType.Catch:
                    vaus.VausState = VausState.Normal;
                    break;
                case PowerUpType.Disruption:
                    break;
                case PowerUpType.Enlarge:
                    vaus.VausState = VausState.Enlarged;
                    break;
                case PowerUpType.Laser:
                    vaus.VausState = VausState.Laser;
                    break;
                case PowerUpType.Player:
                    playerManager.AddLife();
                    break;
                case PowerUpType.Slow:
                    ball.SetSlowSpeed();
                    break;
            }
            Destroy(powerUp.gameObject);
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
    }
}