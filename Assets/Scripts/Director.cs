using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

	GameObject[] players;

	//puts all the agent/player objects in an array
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
	}

	//Does various things depending on what is clicked
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		//used when a player right clicks to give all active agents a target
		if (Input.GetButtonDown ("Fire2")) {
			if (Physics.Raycast(ray, out hit)) {//gets the location clicked on and stores it in hit
				for (int i = 0; i < players.Length; i++) {//iterates through all the players 
					GameObject gameObject = players [i];
					if (gameObject.GetComponent<fullAgent> ().getIsSelected()) {
						gameObject.GetComponent<fullAgent> ().setTarget(hit);//sets the target location for all players that are selected
					}
				}
			}
		}
		//used when a player clicks the left mouse button. Does nothing unless the player clicked on a player object in which case it toggles whether that object is selected
		if (Input.GetButtonDown ("Fire1")) {
			if (Physics.Raycast (ray, out hit)) {
                if (hit.collider.CompareTag("Player"))
                {
                    if (hit.transform.gameObject.GetComponent<fullAgent>().getIsSelected())
                    {
                        hit.transform.gameObject.GetComponent<fullAgent>().isNowNotSelected();
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<fullAgent>().isNowSelected();
                    }
                    GameObject[] navObstacles = GameObject.FindGameObjectsWithTag("NavObstacle");
                    for (int i = 0; i < navObstacles.Length; i++)
                    {
                        navObstacles[i].GetComponent<obstacleScript>().isSelected = false;
                    }
                }
                else if (hit.collider.CompareTag("NavObstacle"))
                {
                    hit.transform.gameObject.GetComponent<obstacleScript>().isSelected = true;
                    GameObject[] navObstacles = GameObject.FindGameObjectsWithTag("NavObstacle");
                    for (int i = 0; i < navObstacles.Length; i++)
                    {
                        if (hit.transform.gameObject != navObstacles[i])
                        {
                            navObstacles[i].GetComponent<obstacleScript>().isSelected = false;
                        }
                    }
                } 
			}
		}
		//clears all selected players when delete is hit
		if (Input.GetButtonDown ("Deselect")) {
			for (int i = 0; i < players.Length; i++) {
				GameObject gameObject = players [i];
				gameObject.GetComponent<fullAgent>().isNowNotSelected();
			}
		}
	}
}
