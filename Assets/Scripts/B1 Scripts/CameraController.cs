using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public void MoveCamera (float horizontalArrow, float verticalArrow, float horizontalMouse, float verticalMouse) {
		Vector3 moveArrow = new Vector3 (horizontalArrow, 0.0f, verticalArrow);
		Vector3 moveMouse = new Vector3 (-verticalMouse, horizontalMouse, 0.0f);
		transform.position += moveArrow * Time.deltaTime * 35.0f;
		transform.Rotate (moveMouse);
	}

	int speed = 100;
	void Update(){
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			transform.Translate(Vector3.back * speed * Time.deltaTime);
		}
	}

}