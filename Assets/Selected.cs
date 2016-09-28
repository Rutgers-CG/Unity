using UnityEngine;
using System.Collections;

public class Selected : MonoBehaviour {

    public bool select;
    private GameObject cube;

    // Use this for initialization
    void Start()
    {
        select = false;
        cube = this.gameObject.transform.FindChild("Cube").gameObject;
        cube.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
        if (Camera.main.GetComponent<NavCharScript>().agent != gameObject.GetComponent<NavMeshAgent>())
        {
            select = false;
        }
        else
        {
            select = true; 
        }

        if (select == true)
        {
            cube.SetActive(true); 
        }

        if (select == false)
        {
            cube.SetActive(false);
        }
	}
}
