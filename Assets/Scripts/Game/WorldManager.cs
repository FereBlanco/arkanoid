using System.Collections.Generic;
using Scripts.Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Game
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private Vaus m_Vaus;
        [SerializeField] private Ball m_Ball;
        [SerializeField] private DeadZone m_DeadZone;
        [SerializeField] private PowerUp[] m_PowerUpPrefabs;
        [SerializeField, Min(0)] private int m_NumberOfPowerUpsToAdd = 0;
        private List<Brick> m_Bricks = new List<Brick>();
        private PlayerManager m_PlayerManager;

        public void Awake()
        {
            Assert.IsNotNull(m_Vaus, "ERROR: vaus not assigned in WorldManager.cs");
            Assert.IsNotNull(m_Ball, "ERROR: ball not assigned in WorldManager.cs");
            Assert.IsNotNull(m_DeadZone, "ERROR: deadZone not assigned in WorldManager.cs");

            SetupBricks();

            m_Vaus.OnBallReleaseEvent += OnBallReleaseCallback;
            m_DeadZone.OnBallExitDeadZoneEvent += OnBallExitDeadZoneCallback;
        }

        public void Start()
        {
            SetupPowerUps();
            m_PlayerManager = PlayerManager.GetInstance().GetComponent<PlayerManager>();
        }

        private void Update()
        {
            // Only to test purposes
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                m_Ball.transform.position = new Vector2(-20.0f, -10.0f);
                m_Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-20.0f, 15.0f);
            }
            // Only to test purposes
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                m_Ball.transform.position = new Vector2(8.0f, -10.0f);
                m_Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(20.0f, 15.0f);
            }
            // Only to test purposes
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("K key");
                m_Vaus.VausState = VausState.Destroyed;
            }
        }

        private void OnBrickDestroyedCallback(Brick brick)
        {
            m_PlayerManager.AddScore(brick.GetScore());
            m_Bricks.Remove(brick);
            brick.OnBrickDestroyedEvent -= OnBrickDestroyedCallback;

            if (brick.HasPowerUp())
            {
                PowerUpType powerUpType = brick.PowerUpType;
                PowerUp newPowerUp = Instantiate(m_PowerUpPrefabs[(int)powerUpType], brick.transform.position, Quaternion.identity);
                newPowerUp.PowerUpType = powerUpType;
                newPowerUp.OnPowerUpActivateEvent += OnPowerUpActivateCallBack;
            }
            Destroy(brick.gameObject);
            // if (bricks.Count == 0) we reach next level
        }

        private void OnBallReleaseCallback()
        {
            m_Ball.Release();
        }

        private void OnPowerUpActivateCallBack(PowerUp powerUp)
        {
            switch (powerUp.PowerUpType)
            {
                case PowerUpType.Break:
                    break;
                case PowerUpType.Catch:
                    m_Vaus.VausState = VausState.Normal;
                    break;
                case PowerUpType.Disruption:
                    break;
                case PowerUpType.Enlarge:
                    m_Vaus.VausState = VausState.Enlarged;
                    break;
                case PowerUpType.Laser:
                    m_Vaus.VausState = VausState.Laser;
                    break;
                case PowerUpType.Player:
                    m_PlayerManager.AddLife();
                    break;
                case PowerUpType.Slow:
                    m_Ball.SetSlowSpeed();
                    break;
            }
            Destroy(powerUp.gameObject);
        }

        private void OnBallExitDeadZoneCallback(GameObject go)
        {
            GameObject[] activeBullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject activeBullet in activeBullets)
            {
                activeBullet.SetActive(false);
            }

            GameObject[] activePowerUps = GameObject.FindGameObjectsWithTag("PowerUp");
            foreach (GameObject activePowerUp in activePowerUps)
            {
                Destroy(activePowerUp);
            }

            m_Vaus.VausState = VausState.Destroyed;
            m_PlayerManager.Lives--;
        }

        private void SetupBricks()
        {
            m_Bricks = new List<Brick>();
            GameObject[] brickGOs = GameObject.FindGameObjectsWithTag("Brick");
            foreach (var brickGO in brickGOs)
            {
                Brick brick = brickGO.GetComponent<Brick>();
                brick.OnBrickDestroyedEvent += OnBrickDestroyedCallback;
                m_Bricks.Add(brick);
            }
        }

        private void SetupPowerUps()
        {
            var bricksWithoutPowerUp = m_Bricks.FindAll(brick => !brick.HasPowerUp());
            // This lambda notation is equal to:
                // List<Brick> bricksWithoutPowerUp = new List<Brick>();
                // foreach (var brick in m_Bricks)
                // {
                //     if (!brick.HasPowerUp())
                //     {
                //         bricksWithoutPowerUp.Add(brick);
                //     }
                // }

            int finalNumberOfPowerUpsToAdd = Mathf.Min(m_NumberOfPowerUpsToAdd, bricksWithoutPowerUp.Count);
            for (int i = 1; i <= finalNumberOfPowerUpsToAdd; i++)
            {
                int randomPowerUpIndex = Random.Range(0, m_PowerUpPrefabs.Length);
                int randomBrickIndex = Random.Range(0, bricksWithoutPowerUp.Count);
                PowerUpType powerUpType = m_PowerUpPrefabs[randomPowerUpIndex].PowerUpType;
                Brick brick = bricksWithoutPowerUp[randomBrickIndex];
                brick.PowerUpType = powerUpType;
                bricksWithoutPowerUp.Remove(brick);
            }
        }

        public void Reset()
        {
            m_Vaus.Reset();
            m_Ball.Reset();
        }
    }
}