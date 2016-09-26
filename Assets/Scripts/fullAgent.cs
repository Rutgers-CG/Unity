using UnityEngine;
using System.Collections;

public class fullAgent : MonoBehaviour {

	Vector3 target;
	NavMeshAgent agent;
	private Ray shootRay;
	bool isSelected;

	//set the initial goal of travel to be not moving anywhere
	void Awake () {
		agent = GetComponent<NavMeshAgent>();
		target = gameObject.transform.position;
	}

	//sends the agent towards the given goal of travel
	void Update () {
		agent.SetDestination (target);
		agent.Resume ();
	}

	//used by director to toggle the agent being selected on
	public void isNowSelected() {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.blue;
		isSelected = true;
	}

	//used by director to toggle the agent being selected off
	public void isNowNotSelected() {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.white;
		isSelected = false;
	}

	public bool getIsSelected() {
		return isSelected;
	}

	//used by Director to set the location of travel
	public void setTarget(RaycastHit givenHit) {
		target = givenHit.point;
	}
}