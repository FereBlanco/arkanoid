using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
    public partial class WorldManager : MonoBehaviour
    {
        [Header ("Game configuration")]
        [SerializeField] protected Vaus m_Vaus;
        [SerializeField] private DeadZone m_DeadZone;
        protected PlayerManager m_PlayerManager;
        protected BallsManager m_BallsManager;

        protected virtual void Awake()
        {
            m_BallsManager = GetComponentInChildren<BallsManager>();
            Assert.IsNotNull(m_BallsManager, "ERROR: m_BallsManager not found in class WorldManager children");
            Assert.IsNotNull(m_Vaus, "ERROR: vaus not assigned in class WorldManager");
            Assert.IsNotNull(m_DeadZone, "ERROR: deadZone not assigned in WorldManager.cs");

            m_Vaus.OnBallReleaseEvent += OnBallReleaseCallback;
            m_DeadZone.OnBallExitDeadZoneEvent += OnBallExitDeadZoneCallback;
        }

        protected virtual void Start()
        {
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

        private void OnBallReleaseCallback()
        {
            m_BallsManager.Release();
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

        internal virtual void Reset()
        {
            StartCoroutine(VausRestore());
        }

        IEnumerator VausRestore()
        {
            yield return new WaitForSeconds(1f);
            m_Vaus.Reset();
            m_BallsManager.Reset();
        }
    }
}