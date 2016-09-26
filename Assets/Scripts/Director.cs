using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

	GameObject[] players;

	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
	}

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetButtonDown ("Fire2")) {
			if (Physics.Raycast(ray, out hit, 100)) {
				for (int i = 0; i < players.Length; i++) {
					GameObject gameObject = players [i];
					if (gameObject.GetComponent<fullAgent> ().isSelected) {
						gameObject.GetComponent<fullAgent> ().setHit(hit);
					}
				}
			}
		}
		if (Input.GetButtonDown ("Fire1")) {
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.CompareTag ("Player")) {
					if (hit.transform.gameObject.GetComponent<fullAgent> ().isSelected) {
						hit.transform.gameObject.GetComponent<fullAgent> ().isNowNotSelected ();
					} else {
						hit.transform.gameObject.GetComponent<fullAgent> ().isNowSelected ();
					}
				} 
			}
		}
		if (Input.GetButtonDown ("Deselect")) {
			for (int i = 0; i < players.Length; i++) {
				GameObject gameObject = players [i];
				gameObject.GetComponent<fullAgent>().isNowNotSelected();
			}
		}
	}
}
