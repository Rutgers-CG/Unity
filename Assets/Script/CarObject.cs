using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObject : MonoBehaviour {

    public Transform targetObj;
    // Use this for initialization
    void Start () {
        GetComponent<NavMeshAgent>().destination = targetObj.position;
        GetComponent<Nave>
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
