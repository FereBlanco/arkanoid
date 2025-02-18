using System.Collections;
using System.Collections.Generic;
using Scripts.Game;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Game
{
    public class WorldManager : MonoBehaviour
    {
        [Header ("Game configuration")]
        [SerializeField] private Vaus m_Vaus;
        [SerializeField] private DeadZone m_DeadZone;
        [SerializeField] private Breakdoor m_Breakdoor;
        [SerializeField] private PowerUp[] m_PowerUpPrefabs;
        [SerializeField, Min(0)] private int m_NumberOfPowerUpsToAdd = 0;
        private List<Brick> m_Bricks = new List<Brick>();
        private PlayerManager m_PlayerManager;
        private BallsManager m_BallsManager;
        private EnemySpawner m_EnemySpawner;
        private bool m_RoundDoh = true;

        public void Awake()
        {
            m_BallsManager = GetComponentInChildren<BallsManager>();
            if (!m_RoundDoh) m_EnemySpawner = GetComponentInChildren<EnemySpawner>();

            Assert.IsNotNull(m_BallsManager, "ERROR: m_BallsManager not found in class WorldManager children");
            if (!m_RoundDoh) Assert.IsNotNull(m_EnemySpawner, "ERROR: m_EnemySpawner not found in class WorldManager children");
            Assert.IsNotNull(m_Breakdoor, "ERROR: m_Breakdoor not found in class WorldManager children");
            Assert.IsNotNull(m_Vaus, "ERROR: vaus not assigned in class WorldManager");
            Assert.IsNotNull(m_DeadZone, "ERROR: deadZone not assigned in WorldManager.cs");

            SetupBricks();

            m_Vaus.OnBallReleaseEvent += OnBallReleaseCallback;
            m_DeadZone.OnBallExitDeadZoneEvent += OnBallExitDeadZoneCallback;
            m_Breakdoor.OnVausEnterBreakdoorEvent += OnVausEnterBreakdoorCallback;
        }


        public void Start()
        {
            SetupPowerUps();
            m_PlayerManager = PlayerManager.Instance.GetComponent<PlayerManager>();
        }

        private void Update()
        {
            // Only to test purposes
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                m_BallsManager.ResetBallLeft();
            }
            // Only to test purposes
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                m_BallsManager.ResetBallRight();
            }
        }

        private void OnBrickDestroyedCallback(Damage damage)
        {
            m_PlayerManager.AddScore(damage.GetScore());
            var brick = damage.GetComponent<Brick>();
            m_Bricks.Remove(brick);
            damage.OnDestroyedEvent -= OnBrickDestroyedCallback;

            if (brick.HasPowerUp())
            {
                PowerUpType powerUpType = brick.PowerUpType;
                PowerUp newPowerUp = Instantiate(m_PowerUpPrefabs[(int)powerUpType], damage.transform.position, Quaternion.identity);
                newPowerUp.PowerUpType = powerUpType;
                newPowerUp.OnPowerUpActivateEvent += OnPowerUpActivateCallBack;
            }
            Destroy(damage.gameObject);
            // When (bricks.Count == 0) we reach next level
        }

        private void OnBallReleaseCallback()
        {
            m_BallsManager.Release();
        }

        private void OnPowerUpActivateCallBack(PowerUp powerUp)
        {
            switch (powerUp.PowerUpType)
            {
                case PowerUpType.Break:
                    Debug.Log("TAKE POWER UP BREAK");
                    m_Breakdoor.Open();
                    break;
                case PowerUpType.Catch:
                    m_Vaus.VausState = VausState.Normal;
                    break;
                case PowerUpType.Disruption:
                    m_BallsManager.Disrupt();
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
                    m_BallsManager.SetSlowSpeed();
                    break;
            }
            Destroy(powerUp.gameObject);
        }

        private void OnBallExitDeadZoneCallback(GameObject go)
        {
            if (m_BallsManager.DestroyBall(go.GetComponent<Ball>()))
            {
                m_Vaus.VausState = VausState.Destroyed;
                m_PlayerManager.Lives--;

                Reset();
            }
        }

        private void OnVausEnterBreakdoorCallback()
        {
            m_Breakdoor.OnVausEnterBreakdoorEvent -= OnVausEnterBreakdoorCallback;
            m_PlayerManager.AddScore(1000);
            LevelManager.Instance.RoundClear();
        }

        private void SetupBricks()
        {
            m_Bricks = new List<Brick>();
            GameObject[] brickGOs = GameObject.FindGameObjectsWithTag("Brick");
            foreach (var brickGO in brickGOs)
            {
                Brick brick = brickGO.GetComponent<Brick>();
                brick.GetComponent<Damage>().OnDestroyedEvent += OnBrickDestroyedCallback;
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
            ResetPowerUps();
            ResetBullets();
            if (!m_RoundDoh) m_EnemySpawner.Reset();

            m_Breakdoor.Close();
            // I should probably differentiate between "Initializing" screens and "Ending" screens
            // (they may not always be the same)
            m_Breakdoor.OnVausEnterBreakdoorEvent += OnVausEnterBreakdoorCallback;

            StartCoroutine(VausRestore());
        }

        IEnumerator VausRestore()
        {
            yield return new WaitForSeconds(1f);
            m_Vaus.Reset();
            m_BallsManager.Reset();
        }

        private void ResetPowerUps()
        {
            var powerUps = GameObject.FindGameObjectsWithTag(Constants.TAG_POWER_UP);
            foreach (var powerUp in powerUps)
            {
                Destroy(powerUp.gameObject);
            }
        }

        private void ResetBullets()
        {
            var bullets = GameObject.FindGameObjectsWithTag(Constants.TAG_BULLET);
            foreach (var bullet in bullets)
            {
                // Bullets use Object Pool
                bullet.SetActive(false);
            }
        }
    }
}