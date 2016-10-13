using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Cameras
{
    public class FreeLookCam : PivotBasedCameraRig
    {
        // This script is designed to be placed on the root object of a camera rig,
        // comprising 3 gameobjects, each parented to the next:

        // 	Camera Rig
        // 		Pivot
        // 			Camera

        [SerializeField] private float m_MoveSpeed = 1f;                      // How fast the rig will move to keep up with the target's position.
        [Range(0f, 10f)] [SerializeField] private float m_TurnSpeed = 1.5f;   // How fast the rig will rotate from user input.
        [SerializeField] private float m_TurnSmoothing = 0.0f;                // How much smoothing to apply to the turn input, to reduce mouse-turn jerkiness
		[SerializeField] private float m_TiltMax = 0f;//75f;                       // The maximum value of the x axis rotation of the pivot.
		[SerializeField] private float m_TiltMin = 0f;//45f;                       // The minimum value of the x axis rotation of the pivot.
        [SerializeField] private bool m_LockCursor = false;                   // Whether the cursor should be hidden and locked.
        [SerializeField] private bool m_VerticalAutoReturn = false;           // set wether or not the vertical axis should auto return

        private float m_LookAngle;                    // The rig's y axis rotation.
		private float m_LookVerticalAngle;
        private float m_TiltAngle;                    // The pivot's x axis rotation.
        private const float k_LookDistance = 100f;    // How far in front of the pivot the character's look target is.
		private Vector3 m_PivotEulers;
		private Quaternion m_PivotTargetRot;
		private Quaternion m_TransformTargetRot;

		private GameObject Target;

        protected override void Awake()
        {
            base.Awake();
            // Lock or unlock the cursor.
            Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !m_LockCursor;
			m_PivotEulers = m_Pivot.rotation.eulerAngles;

	        m_PivotTargetRot = m_Pivot.transform.localRotation;
			m_TransformTargetRot = transform.localRotation;

			Vector3 initialCameraTranslation = new Vector3(0, 10, 0);
			Vector3 initialCameraRotation = new Vector3(0, 90, 0);

			transform.Translate (initialCameraTranslation);
			//m_Pivot.localRotation = Quaternion.Euler(initialCameraRotation);
        }

		protected void Start()
		{
			Target = GameObject.FindGameObjectWithTag ("Erika");
		}



        protected void Update()
        {
			ThirdPersonCamera ();
            HandleRotationMovement();
            if (m_LockCursor && Input.GetMouseButtonUp(0))
            {
                Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = !m_LockCursor;
            }
        }


        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

		void ThirdPersonCamera()
		{
			Vector3 pos;
			transform.rotation = Target.transform.rotation;
			pos = Target.transform.position;
			pos += transform.up * 1.8f;
			pos += transform.forward * -2.6f;
			pos += transform.right * 0.2f;
			transform.position = pos;
		}


        protected override void FollowTarget(float deltaTime)
        {
            if (m_Target == null) return;
            // Move the rig towards target position.
            transform.position = Vector3.Lerp(transform.position, m_Target.position, deltaTime*m_MoveSpeed);
        }


        private void HandleRotationMovement()
        {
			if(Time.timeScale < float.Epsilon)
			return;

            // Read the user input
			float x = 0;
			float y = 0;
			float v = 0;
			if (Input.GetMouseButton (2)) {
				x = CrossPlatformInputManager.GetAxis ("Mouse X");
				y = 0.5f * CrossPlatformInputManager.GetAxis ("Mouse Y");
				v = -CrossPlatformInputManager.GetAxis ("Mouse Y");
			}
            // Adjust the look angle by an amount proportional to the turn speed and horizontal input.
            m_LookAngle += x*m_TurnSpeed;
			m_LookVerticalAngle += v * m_TurnSpeed;

            // Rotate the rig (the root object) around Y axis only:
			m_TransformTargetRot = Quaternion.Euler(m_LookVerticalAngle, m_LookAngle, 0f);

            if (m_VerticalAutoReturn)
            {
                // For tilt input, we need to behave differently depending on whether we're using mouse or touch input:
                // on mobile, vertical input is directly mapped to tilt value, so it springs back automatically when the look input is released
                // we have to test whether above or below zero because we want to auto-return to zero even if min and max are not symmetrical.
                m_TiltAngle = y > 0 ? Mathf.Lerp(0, -m_TiltMin, y) : Mathf.Lerp(0, m_TiltMax, -y);
            }
            else
            {
                // on platforms with a mouse, we adjust the current angle based on Y mouse input and turn speed
                m_TiltAngle -= y*m_TurnSpeed;
                // and make sure the new value is within the tilt range
                m_TiltAngle = Mathf.Clamp(m_TiltAngle, -m_TiltMin, m_TiltMax);
            }

            // Tilt input around X is applied to the pivot (the child of this object)

			//m_PivotTargetRot = Quaternion.Euler(m_TiltAngle, m_PivotEulers.y , m_PivotEulers.z);

			if (m_TurnSmoothing > 0)
			{
				m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_PivotTargetRot, m_TurnSmoothing * Time.deltaTime);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, m_TransformTargetRot, m_TurnSmoothing * Time.deltaTime);
			}
			else
			{
				m_Pivot.localRotation = m_PivotTargetRot;
				transform.localRotation = m_TransformTargetRot;
			}

			//float moveH = Input.GetAxis ("Horizontal");
			//float moveV = 15*CrossPlatformInputManager.GetAxis("Mouse ScrollWheel") + Input.GetAxis ("Vertical");
			//float moveV = Input.GetAxis ("Vertical");
			//Vector3 cameraTranslation = new Vector3(moveH, 0, moveV);
			//transform.Translate (cameraTranslation);
        }
    }
}
