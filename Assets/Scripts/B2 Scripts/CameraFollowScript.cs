using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform follow;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		follow = GameObject.FindWithTag ("Brute").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
		targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		transform.LookAt (follow);
	}
}
