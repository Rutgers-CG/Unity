using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	Animator animator;
	CharacterController charcontroller;

	[System.Serializable]
	public class AnimationSettings {
		public string verticalVelocityFloat = "Forward";
		public string horizontalVelocityFloat = "Strafe";
		public string groundedBool = "isGrounded";
		public string jumpBool = "isJumping";
		public string walkBool = "isWalking";
		public string runBool = "isRunning";
		public string turnBool = "isTurning"; 
	}

	[SerializeField]
	public AnimationSettings animations;

	[System.Serializable]
	public class PhysicsSettings {
		public float gravityModifier = 9.81f;
		public float baseGravity = 50.0f;
		public float resetGravityValue = 5.0f;
	}

	[SerializeField]
	public PhysicsSettings physics;

	[System.Serializable]
	public class MovementSettings {
		public float jumpSpeed = 6.0f;
		public float jumpTime = 0.25f;
	}

	[SerializeField]
	public MovementSettings movement;

	bool isJumping;
	bool isRunning;
	bool isWalking;
	bool isTurning;
	bool resetGravity;
	float gravity;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		charcontroller = GetComponent<CharacterController> ();
		SetupAnimator ();
	}
		
	// Update is called once per frame
	void Update () {
		ApplyGravity ();

		if (Input.GetKey (KeyCode.X)) {
			isWalking = false;
			isRunning = true;
			if (Input.GetKey (KeyCode.C)) {
				isTurning = true;
				isRunning = false;
			} else {
				isTurning = false;
				isRunning = true;
			}
		} else {
			isWalking = true;
			isRunning = false;
			if (Input.GetKey (KeyCode.C)) {
				isTurning = true;
				isWalking = false;
			} else {
				isTurning = false;
				isWalking = true;
			}
		}

		Animate (Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

		if (Input.GetButtonDown ("Jump")) {
			Jump ();
		}


	}

	public void Animate (float forward, float strafe) {
		animator.SetFloat (animations.verticalVelocityFloat, forward);
		animator.SetFloat (animations.horizontalVelocityFloat, strafe);
		animator.SetBool (animations.groundedBool, charcontroller.isGrounded);
		animator.SetBool (animations.jumpBool, isJumping);
		animator.SetBool (animations.walkBool, isWalking);
		animator.SetBool (animations.runBool, isRunning);
		animator.SetBool (animations.turnBool, isTurning);
	}

	public void Jump() {
		if (isJumping) {
			return;
		}
		if (charcontroller.isGrounded) {
			isJumping = true;
			StartCoroutine (StopJump());
		}
	}

	IEnumerator StopJump() {
		yield return new WaitForSeconds (movement.jumpTime);
		isJumping = false;
	}

	void ApplyGravity() {
		if (!charcontroller.isGrounded) {
			if (!resetGravity) {
				gravity = physics.resetGravityValue;
				resetGravity = true;
			}
			gravity += Time.deltaTime * physics.gravityModifier;
		} else {
			gravity = physics.baseGravity;
			resetGravity = false;
		}
		Vector3 gravityVector = new Vector3();

		if (!isJumping) {
			gravityVector.y -= gravity;
		} else {
			gravityVector.y = movement.jumpSpeed;
		}
		charcontroller.Move (gravityVector * Time.deltaTime);
	}

	// Setup the animator with the child avatar
	void SetupAnimator () {
		Animator[] animators = GetComponentsInChildren<Animator> ();

		if (animators.Length > 0) {
			for (int i = 0; i < animators.Length; i++) {
				Animator anim = animators [i];
				Avatar av = anim.avatar;

				if (anim != animator) {
					animator.avatar = av;
					Destroy (anim);
				}
			}
		}
	}
}