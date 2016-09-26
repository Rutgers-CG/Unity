using UnityEngine;
using System.Collections;

public class fullAgent : MonoBehaviour {

	Transform target;
	NavMeshAgent agent;
	private Ray shootRay;
	public bool isSelected;

	void Awake () {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetButtonDown ("Fire2")) {
			if (Physics.Raycast(ray, out hit, 100) && isSelected) {
				agent.SetDestination(hit.point);
				agent.Resume();
			}
		}
		if (Input.GetButtonDown ("Fire1")) {
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.CompareTag ("Player")) {
					Renderer rend = GetComponent<Renderer> ();
					rend.material.color = Color.blue;
					isSelected = true;
				} 
			}
		}
		if (Input.GetButtonDown ("Deselect")) {
			Renderer rend = GetComponent<Renderer> ();
			rend.material.color = Color.white;
			isSelected = false; 
		}
	}
}