using UnityEngine;

// this comes from the Unity Standard Assets
namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        public float shakeTimer;
        public float shakeAmount;

        [SerializeField]
        private float xMin;
        [SerializeField]
        private float xMax;
        [SerializeField]
        private float yMin;
        [SerializeField]
        private float yMax;

		// private variables
        float m_OffsetZ;
        Vector3 m_LastTargetPosition;
        Vector3 m_CurrentVelocity;
        Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;

			// if target not set, then set it to the player
			if (target==null) {
				target = GameObject.FindGameObjectWithTag("Player").transform;
			}

			if (target==null)
				Debug.LogError("Target not set on Camera2DFollow.");

        }

        // Update is called once per frame
		private void Update()
        {
			if (target == null)
				return;

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
				m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
            
            //if(target.position.x > xMin && target.position.x < xMax)
            transform.position = newPos;
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), -10);

            m_LastTargetPosition = target.position;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                ShakeCamera(0.1f, 0.5f);
            }

            if (shakeTimer >= 0)
            {
                Vector2 shakePos = Random.insideUnitCircle * shakeAmount;
                transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);

                shakeTimer -= Time.deltaTime;
            }
        }

        public void ShakeCamera(float shakePwr, float shakeDur)
        {
            shakeAmount = shakePwr;
            shakeTimer = shakeDur;
        }
    }
}
