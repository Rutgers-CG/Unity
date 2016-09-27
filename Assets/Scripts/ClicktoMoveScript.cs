using UnityEngine;
using System.Collections;


public class ClicktoMoveScript : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private Renderer matRender;
	private Color origColor;
	private Color highlightColor;
	private bool isSelected;

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
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
					navMeshAgent.destination = hit.point;
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