using UnityEngine;
using System.Collections;

public class fullAgent : MonoBehaviour {

	Transform target;
	NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		agent.SetDestination(target.position);
	}
}
