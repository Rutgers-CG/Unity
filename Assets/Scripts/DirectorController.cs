using UnityEngine;
using System.Collections;

public class DirectorController : MonoBehaviour {

	//public Camera mainCam;
	public CameraController camControl;
	//private Vector3 currentCamPos;

	void Start () {
		//currentCamPos = mainCam.transform.position;
	}

	void FixedUpdate () {
		if (Input.GetKey(KeyCode.Z)) {
			camControl.MoveCamera (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));
		}
	}
}