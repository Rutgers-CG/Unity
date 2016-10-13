using UnityEngine;
using System.Collections;

namespace Character {
	public class CharacterInput : MonoBehaviour {

		Animator ErikaAnimator;
		private static float MAX_SPEED = 3.5f;
		private static float MIN_SPEED = -3.5f;
		string speedParam = "Speed";
		string directionParam = "Direction";

		float speed = 0.0f;
		float direction = 0.0f;

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
			if (Input.GetKey ((KeyCode)INPUT_KEY.RUN)) 
			{
				speed = speed*3;
			} 
			else if (Input.GetKey ((KeyCode)INPUT_KEY.DUCK)) 
			{
				
			} 
			else 
			{
				
			}
			// forward motion
			if (Input.GetKey ((KeyCode)INPUT_KEY.FORWARD) && Input.GetKey ((KeyCode)INPUT_KEY.RUN)) {
				speed = 3;
			}
			else if (Input.GetKey ((KeyCode)INPUT_KEY.FORWARD)) {
				//ErikaAnimator.SetFloat (directionParam, 1);
				speed = 1;
			} else if (Input.GetKey ((KeyCode)INPUT_KEY.BACKWARD)) {
				speed = -1;
			} 
			else 
			{
				speed = 0;
				
			}

			// turning
			if (Input.GetKey ((KeyCode)INPUT_KEY.LEFT)) 
			{
				
			} 
			else if (Input.GetKey ((KeyCode)INPUT_KEY.RIGHT)) 
			{
				
			} 
			else 
			{
				
			}

			// jumping
			if (Input.GetKeyDown((KeyCode)INPUT_KEY.JUMP)) 
			{ // Note we only read jump once
				
			}
			if (speed > MAX_SPEED) {
				speed = MAX_SPEED;
			}
			if (speed < MIN_SPEED) {
				speed = MIN_SPEED;
			}
			Debug.Log(ErikaAnimator.GetFloat(speedParam));
			ErikaAnimator.SetFloat (speedParam, speed);
			ErikaAnimator.SetFloat (directionParam, direction);
		
		}
	}
}
