using System.Collections;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyMovement : MonoBehaviour
    {
        private enum EnemyState
        {
            Stopped, Left, Right, Down
        }

        [SerializeField] private EnemyState m_State;
        private EnemyState State
        {
            get => m_State;
            set
            {
                Debug.Log("Try new state: " + value);
                if (value != m_State)
                {
                    m_State = value;
                    UpdateVelocity();
                }
            }
        }

        private Rigidbody2D m_Rigidbody2D;
        private Collider2D m_Collider;
        private float m_HeightCheckStart;
        private float m_HeightFactor = 1.2f;
        private float m_RayLength = 0.1f;
        private float m_Speed = 5f;

        private Vector2 m_DownCheckVector;
        private float m_DumbTime = 2f;

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            // m_OldState = EnemyState.Stopped;
            State = EnemyState.Down;

            m_Collider = GetComponent<Collider2D>();
            m_HeightCheckStart = m_HeightFactor * m_Collider.bounds.extents.y;
            m_DownCheckVector = m_RayLength * Vector2.down;
        }


        public void FixedUpdate()
        {
            switch (State)
            {
                case EnemyState.Stopped:
                    break;
                case EnemyState.Left:
                    break;
                case EnemyState.Right:
                    break;
                case EnemyState.Down:
                    Vector2 position = transform.position + m_HeightCheckStart * Vector3.down;
                    Debug.DrawRay(position, m_DownCheckVector, Color.red);
                    var hit = Physics2D.Raycast(position, m_DownCheckVector, m_RayLength);

                    if (null != hit.transform)
                    {
                        // Debug.Log(hit.transform);
                        State = EnemyState.Stopped;
                    }
                    break;
            }
        }

        private void UpdateVelocity()
        {
            Debug.Log("UpdateVelocity: " + State);
            switch (State)
            {
                case EnemyState.Stopped:
                    m_Rigidbody2D.velocity = Vector2.zero;
                    StartCoroutine(DumbTimeAndGoSide());
                    break;
                case EnemyState.Left:
                    m_Rigidbody2D.velocity = -1f * m_Speed * Vector2.right;
                    break;
                case EnemyState.Right:
                    m_Rigidbody2D.velocity = m_Speed * Vector2.right;
                    break;
                case EnemyState.Down:
                    m_Rigidbody2D.velocity = m_Speed * Vector2.down;
                    break;
            }
        }
        IEnumerator DumbTimeAndGoSide()
        {
            Debug.Log("DumbTimeAndGoSide");
            yield return new WaitForSeconds(m_DumbTime);
            int sideRandom = Random.Range(0, 2);
            State = (sideRandom == 0 ? EnemyState.Left : EnemyState.Right);
        }
    }
}
