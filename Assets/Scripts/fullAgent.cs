using UnityEngine;
using System.Collections;

public class fullAgent : MonoBehaviour {

	public Vector3 hit;
	NavMeshAgent agent;
	private Ray shootRay;
	public bool isSelected;

	void Awake () {
		agent = GetComponent<NavMeshAgent>();
		hit = gameObject.transform.position;
	}

	void Update () {
		if (isSelected) {
			agent.SetDestination (hit);
			agent.Resume ();
		}
	}

	public void isNowSelected() {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.blue;
		isSelected = true;
	}

	public void isNowNotSelected() {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.white;
		isSelected = false;
	}

	public void setHit(RaycastHit givenHit) {
		hit = givenHit.point;
	}
}