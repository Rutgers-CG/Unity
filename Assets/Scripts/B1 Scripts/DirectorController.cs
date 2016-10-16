using UnityEngine;
using System.Collections;

public class DirectorController : MonoBehaviour {

	public CameraController camControl;
	public GameObject[] allAgents;
	public Hashtable stoppedAgents;
	public bool beginBrakes;

	void Awake () {
		beginBrakes = false;
		allAgents = GameObject.FindGameObjectsWithTag ("Agent");
	}

	void Update () {
		if (Input.GetKey (KeyCode.Z)) {
			float horizontalArrow = Input.GetAxis ("Horizontal");
			float verticalArrow = Input.GetAxis ("Vertical");
			float horizontalMouse = Input.GetAxis ("Mouse X");
			float verticalMouse = Input.GetAxis ("Mouse Y");
			camControl.MoveCamera (horizontalArrow, verticalArrow, horizontalMouse, verticalMouse);
		} 
		if (beginBrakes) {
			foreach (GameObject agent in allAgents) {
				if (!stoppedAgents.ContainsKey (agent.name)) {
					foreach (string agentName in stoppedAgents.Keys) {
						GameObject stoppedAgent = (GameObject)stoppedAgents [agentName];
						if (Vector3.Distance (agent.transform.position, stoppedAgent.transform.position) <= 5.0f) {
							stoppedAgents.Add (agent.name, agent);
							agent.GetComponent<NavMeshAgent> ().Stop ();
							break;
						}
					}
				}
			}
		}
	}
}