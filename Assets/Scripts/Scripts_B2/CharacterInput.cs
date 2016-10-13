using UnityEngine;
using System.Collections;

namespace Character {
	public class CharacterInput : MonoBehaviour {

		Animator ErikaAnimator;
		private float MAX_SPEED = 1.0f;
		private float MIN_SPEED = -3.5f;
		private float MAX_STRAFE = 0.5f;
		private float MIN_STRAFE = -0.5f;
		string speedParam = "Speedx";
		string directionParam = "Speedz";
		bool finishedJump = true;

		float speedx = 0.0f;
		float speedz = 0.0f;

		public enum INPUT_KEY {
			FORWARD = KeyCode.W,
			BACKWARD = KeyCode.S,
			LEFT = KeyCode.A,
			RIGHT = KeyCode.D,
			DUCK = KeyCode.LeftControl,
			JUMP = KeyCode.Space,
			RUN = KeyCode.LeftShift,
			INVENTORY = KeyCode.I,
			MAP = KeyCode.M,
			FOCUS_LOOK = KeyCode.Mouse1, // secondary mouse button
			CONTEXT_ACTION = KeyCode.Mouse1,
			SELECT_AGENT = KeyCode.Mouse0,
			FREE_CAM = KeyCode.F1,
			THIRD_CAM = KeyCode.F2,
			FIRST_CAM = KeyCode.F3
		}

		// Use this for initialization
		void Start () {
			ErikaAnimator = gameObject.GetComponent<Animator> ();
		}
		
		// Update is called once per frame
		void Update () {
				
			// forward motion
			if (Input.GetKey ((KeyCode)INPUT_KEY.FORWARD) && Input.GetKey ((KeyCode)INPUT_KEY.RUN)) {
				speedx += 0.01f;
				MAX_SPEED = 1;
			}
			else if (Input.GetKey ((KeyCode)INPUT_KEY.FORWARD)) {
				//ErikaAnimator.SetFloat (directionParam, 1);
				speedx +=0.01f;
				MAX_SPEED = 0.5f;
			} else if (Input.GetKey ((KeyCode)INPUT_KEY.BACKWARD)) {
				speedx -= 0.01f;
				MIN_SPEED = -0.5f;
			} 
			else 
			{
				MIN_SPEED = 0.0f;
				MAX_SPEED = 0.0f;
				if (speedx > 0) {
					speedx -= 0.01f;
				} else {
					speedx += 0.01f;
				}
				
			}

			// turning
			if (Input.GetKey ((KeyCode)INPUT_KEY.LEFT)) 
			{
				MAX_STRAFE = 0.5f;
				speedz += 0.01f;
			} 
			else if (Input.GetKey ((KeyCode)INPUT_KEY.RIGHT)) 
			{
				MIN_STRAFE = -0.5f;
				speedz -= 0.01f;
			} 
			else 
			{
				MIN_STRAFE = 0.0f;
				MAX_STRAFE = 0.0f;
				if (speedz > 0) {
					speedz -= 0.01f;
				} else {
					speedz += 0.01f;
				}
					
			}

			// jumping
			if (Input.GetKeyDown((KeyCode)INPUT_KEY.JUMP)) 
			{ // Note we only read jump once
				
			}
			if (speedx > MAX_SPEED) {
				speedx -= 0.02f;
			}
			if (speedx < MIN_SPEED) {
				speedx += 0.02f;
			}
			if (speedz > MAX_STRAFE) {
				speedz -= 0.02f;
			}
			if (speedz < MIN_STRAFE) {
				speedz += 0.02f;
			}

			if (Input.GetKey ((KeyCode)INPUT_KEY.JUMP)) 
			{
				if (speedx > 0.5f) {
					ErikaAnimator.SetTrigger ("RunJump");
				} else if (speedx > 0.3f) {
					ErikaAnimator.SetTrigger ("Jump");
				} else {
					ErikaAnimator.SetTrigger ("IdleJump");
				}
				print (ErikaAnimator.GetBool ("FinishedJump"));
				ErikaAnimator.SetBool ("FinishedJump", true);
				print (ErikaAnimator.GetBool ("FinishedJump"));
			}

			Debug.Log(ErikaAnimator.GetFloat(directionParam));
			ErikaAnimator.SetFloat (speedParam, speedx);
			ErikaAnimator.SetFloat (directionParam, speedz);
		
		}
	}
}
