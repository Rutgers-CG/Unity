using UnityEngine;
using System.Collections;

public class fullAgent : MonoBehaviour {

	Transform target;
	NavMeshAgent agent;
	private Ray shootRay;

	void Awake () {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetButtonDown ("Fire2")) {
			if (Physics.Raycast(ray, out hit, 100)) {
				target = hit.transform;
				agent.SetDestination(hit.point);
				agent.Resume();
			}
		}
	}
}