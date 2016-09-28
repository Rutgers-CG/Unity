using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Director : MonoBehaviour {

    public List<NavMeshAgent> agents;

    // Use this for initialization
    void Start () {
        agents = new List<NavMeshAgent>(); 
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void SetSelected()
    {

    }

    public void addAgent()
    {
        agents.Add(Camera.main.GetComponent<NavCharScript>().agent);
        Camera.main.GetComponent<NavCharScript>().agent.GetComponent<Selected>().select = true;
    }

    public void clearAgents()
    {
        foreach(NavMeshAgent a in agents)
        {
            a.GetComponent<Selected>().select = false;
        }
        agents = new List<NavMeshAgent>(); 
    }

}
