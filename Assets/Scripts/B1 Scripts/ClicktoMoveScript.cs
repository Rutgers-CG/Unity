using UnityEngine;
using System.Collections;


public class ClicktoMoveScript : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private Renderer matRender;
	private Color origColor;
	private Color highlightColor;
	private bool isSelected;
	public DirectorController director;

	// Use this for initialization
	void Awake () {
		navMeshAgent = GetComponent<NavMeshAgent> ();
		matRender = GetComponent<Renderer> ();
		origColor = matRender.material.color;
		highlightColor = Color.white;
		isSelected = false;
	}

	// Update is called once per frame
	void Update () {
		if (isSelected) {
			if (Input.GetMouseButtonDown (0)) {
				if (director.beginBrakes == true) {
					director.beginBrakes = false;
					director.stoppedAgents.Clear ();
				}
				navMeshAgent.Resume ();
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
					navMeshAgent.destination = hit.point;
				}
			} else if (Vector3.Distance(transform.position,navMeshAgent.destination) < 1.0f) {
				if (director.beginBrakes != true) {
					navMeshAgent.Stop ();
					director.beginBrakes = true;
					director.stoppedAgents = new Hashtable ();
					director.stoppedAgents.Add (gameObject.name, gameObject);
				}
			} 
		}
	}

	void OnMouseEnter () {
		matRender.material.color = highlightColor;
	}

	void OnMouseExit () {
		if(!isSelected) 
			matRender.material.color = origColor;
	}

	void OnMouseDown () {
		if (!isSelected)
			isSelected = true;
		else
			isSelected = false;
	}
}