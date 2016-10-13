using UnityEngine;
using System.Collections;

public class MouseCharacterController : MonoBehaviour {

	bool isSelected;
    Vector3 target;
    NavMeshAgent navAgent;
    // Use this for initialization
    void Start () {

		isSelected = false;
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        // monitor the mouse activity, right click on the Goblin will pick the Goblin 
        // and right click on other place will deselect it
		if( Input.GetMouseButtonDown(1)){
			//Debug.Log("Mouse is down");
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "Goblin")
				{
					Debug.Log ("Goblin selected!");
					isSelected = true;
				} else {
					Debug.Log ("de selected!");
                    isSelected = false;
				}
			} else {
				Debug.Log("No hit");
			}
		}

        // monitor the mouse activity, left click to set a target
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (isSelected)
                {
                    target = hit.point;
                    Debug.Log(target);
					transform.LookAt (target);
                    navAgent.SetDestination(target);
                }
            }
        }
    }
}
