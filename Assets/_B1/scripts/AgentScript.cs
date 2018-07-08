using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour {

    //public Transform target;
    NavMeshAgent agent;
    protected bool stopped = false;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void makeStop()
    {
        stopped = true;
    }

    public bool isStop()
    {
        return stopped;
    }

    public void unStop()
    {
        stopped = false;
    }

}
