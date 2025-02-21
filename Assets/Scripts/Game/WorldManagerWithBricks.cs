using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public class WorldManagerWithBricks : WorldManager
    {
        [SerializeField] private PowerUp[] m_PowerUpPrefabs;
        [SerializeField, Min(0)] private int m_NumberOfPowerUpsToAdd = 0;
        [SerializeField] private Breakdoor m_Breakdoor;
        private List<Brick> m_Bricks = new List<Brick>();
        private EnemySpawner m_EnemySpawner;

        protected override void Awake()
        {
            base.Awake();

            m_EnemySpawner = GetComponentInChildren<EnemySpawner>();
            Assert.IsNotNull(m_EnemySpawner, "ERROR: m_EnemySpawner not found in class WorldManager children");
            Assert.IsNotNull(m_Breakdoor, "ERROR: m_Breakdoor not found in class WorldManager children");

            m_Breakdoor.OnVausEnterBreakdoorEvent += OnVausEnterBreakdoorCallback;
            SetupBricks();
        }

        protected override void Start()
        {
            SetupPowerUps();
            base.Start();
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

        internal override void Reset()
        {
            ResetPowerUps();
            ResetBullets();
            m_EnemySpawner.Reset();
            m_Breakdoor.Close();
            m_Breakdoor.OnVausEnterBreakdoorEvent += OnVausEnterBreakdoorCallback;

            base.Reset();
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
            var bulletGOs = GameObject.FindGameObjectsWithTag(Constants.TAG_BULLET);
            foreach (var bulletGO in bulletGOs)
            {
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                bullet.SetInactive();
            }
        }

        private void OnPowerUpActivateCallBack(PowerUp powerUp)
        {
            switch (powerUp.PowerUpType)
            {
                case PowerUpType.Break:
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

        private void OnVausEnterBreakdoorCallback()
        {
            m_Breakdoor.OnVausEnterBreakdoorEvent -= OnVausEnterBreakdoorCallback;
            m_PlayerManager.AddScore(1000);
            LevelManager.Instance.RoundClear();
        }
    }
}