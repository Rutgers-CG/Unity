using UnityEngine;
using System.Collections;

public class fullAgent : MonoBehaviour {

	Vector3 target;

	NavMeshAgent agent;
	NavMeshObstacle obs;

	private Ray shootRay;
	bool isSelected;

	//bool isInMotion = false;
	bool isArrived = true;

    public static float nbrInTarget = 0;
	public static int selectedTotal = 0;

	public void stop(){
		agent.Stop();
	}

    void OnTriggerEnter(Collider other)
    {
		if (!isArrived) {
			agent.Stop ();
			Debug.Log ("hitted by agent");
		}
    }

    //set the initial goal of travel to be not moving anywhere
    void Awake () {
		agent = GetComponent<NavMeshAgent>();
		obs = GetComponent<NavMeshObstacle>();

		target = gameObject.transform.position;
	}

	//sends the agent towards the given goal of travel
	void Update () {
		//agent.SetDestination (target);
		if (Vector3.Distance (transform.position, target) < 2.0f) {
			agent.enabled = false;
			obs.enabled = true;
		} else {
			agent.enabled = true;
			obs.enabled = false;
		}
	}

	//used by director to toggle the agent being selected on
	public void isNowSelected() {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.blue;
		isSelected = true;
		selectedTotal += 1;
		//Debug.Log (selectedTotal);
	}

	//used by director to toggle the agent being selected off
	public void isNowNotSelected() {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.white;
		isSelected = false;
		selectedTotal -= 1;
	}

	public bool getIsSelected() {
		return isSelected;
	}

	//used by Director to set the location of travel
	public void setTarget(RaycastHit givenHit) {
		target = givenHit.point;

		isArrived = false;
		agent.enabled = true;
		obs.enabled = false;
		agent.SetDestination (target);

	}
}