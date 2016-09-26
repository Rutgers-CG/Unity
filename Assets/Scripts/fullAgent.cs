using UnityEngine;
using System.Collections;

public class fullAgent : MonoBehaviour {

	RaycastHit hit;
	RaycastHit hitBeforeSelected;
	NavMeshAgent agent;
	private Ray shootRay;
	public bool isSelected;
	GameObject director;

	void Awake () {
		director = GameObject.Find ("EventSystem");
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		if (isSelected) {
			hit = director.GetComponent<Director> ().target;
			if (!hit.Equals(hitBeforeSelected)) {
				agent.SetDestination (hit.point);
				agent.Resume ();
			}
		}
	}

	public void isNowSelected() {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.blue;
		isSelected = true;
		hitBeforeSelected = director.GetComponent<Director> ().target;
	}

	public void isNowNotSelected() {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.white;
		isSelected = false;
	}
}