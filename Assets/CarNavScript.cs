using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNavScript : MonoBehaviour {

    public Transform targetObj;
    // Use this for initialization
    void Start () {
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = targetObj.position;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
