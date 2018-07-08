using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director : MonoBehaviour {

    private bool somethingSelected;
    public GameObject[] AgentList;
    public List<GameObject> selectedList;
    public List<GameObject> tempList;

    private Vector2 mouse1stPoint;
    private Vector2 mouse2ndPoint;
    private bool hold;
    private bool barrierSelected;
    private Renderer rend;

    // Use this for initialization
    void Start () {
        somethingSelected = false;
        if (AgentList.Length == 0)
        {
            AgentList = GameObject.FindGameObjectsWithTag("Agent");
        }

        mouse1stPoint = new Vector2(0, 0);
        mouse2ndPoint = new Vector2(0, 0);
        hold = false;

        foreach (GameObject agent in AgentList)
        {
            rend = agent.GetComponent<Renderer>();
            rend.material.color = Color.black;
        }

        barrierSelected = false;

        selectedList = new List<GameObject>();
        tempList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        foreach (GameObject agentOuter in selectedList)
        {
            foreach (GameObject agentInner in selectedList)
            {
                if (agentInner != agentOuter && (!agentInner.GetComponent<AgentScript>().isStop() || !agentOuter.GetComponent<AgentScript>().isStop()))
                {
                    if (agentInner.GetComponent<NavMeshAgent>().destination == agentOuter.GetComponent<NavMeshAgent>().destination)
                    {

                        if ((Mathf.Abs((agentInner.transform.position.x - agentInner.GetComponent<NavMeshAgent>().destination.x)) <= (agentInner.GetComponent<CapsuleCollider>().radius)
                            && Mathf.Abs(agentInner.transform.position.z - agentInner.GetComponent<NavMeshAgent>().destination.z) <= (agentInner.GetComponent<CapsuleCollider>().radius)))
                        {
                            agentInner.GetComponent<AgentScript>().makeStop();
                            agentInner.GetComponent<NavMeshAgent>().Stop();
                        }

                        if (((agentInner.GetComponent<NavMeshAgent>().destination == agentInner.transform.position) || agentInner.GetComponent<AgentScript>().isStop() || agentOuter.GetComponent<AgentScript>().isStop())
                            && ((agentInner.GetComponent<NavMeshAgent>().destination.x - agentInner.transform.position.x) > 10*Mathf.Sin(3.14f/selectedList.Count) && (agentInner.GetComponent<NavMeshAgent>().destination.y - agentInner.transform.position.y) > 10 * Mathf.Sin(3.14f / selectedList.Count)))
                        {
                            agentInner.GetComponent<AgentScript>().makeStop();
                            agentInner.GetComponent<NavMeshAgent>().Stop();
                        }
                    }
                }
            }
        }


        if (somethingSelected == true)
        {
            if (Input.GetMouseButtonUp(0) && barrierSelected == false)
            {
                foreach (GameObject agent in selectedList)
                {
                    if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag != "Agent")
                    {
                        agent.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                        

                    }
                }
                somethingSelected = false;
            }
            if (barrierSelected == true)
            {
                MainCamera.BarrierSelected = true;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    foreach (GameObject barrier in selectedList)
                    {
                        Vector3 position = barrier.transform.position;
                        position.x++;
                        barrier.transform.position = position;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    foreach (GameObject barrier in selectedList)
                    {
                        Vector3 position = barrier.transform.position;
                        position.x--;
                        barrier.transform.position = position;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    foreach (GameObject barrier in selectedList)
                    {
                        Vector3 position = barrier.transform.position;
                        position.z++;
                        barrier.transform.position = position;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    foreach (GameObject barrier in selectedList)
                    {
                        Vector3 position = barrier.transform.position;
                        position.z--;
                        barrier.transform.position = position;
                    }
                }

                if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag != "Barrier")
                {
                    foreach (GameObject barrier in selectedList)
                    {
                        rend = barrier.GetComponent<Renderer>();
                        rend.material.color = Color.white;
                    }

                    MainCamera.BarrierSelected = false;
                    barrierSelected = false;
                    selectedList.Clear();

                }

            }
        }

        if (Input.GetMouseButtonDown(0) && hold == false && somethingSelected == false)
        {
            mouse1stPoint = Input.mousePosition;
            hold = true;
        }
        else if (Input.GetMouseButtonUp(0) && hold == true && somethingSelected == false)
        {

            if (AgentList != null)
            {
                foreach (GameObject agent in AgentList)
                {
                    rend = agent.GetComponent<Renderer>();
                    rend.material.color = Color.black;
                }
            }

            selectedList.Clear();
            mouse2ndPoint = Input.mousePosition;

            if (AgentList != null)
            {
                foreach (GameObject agent in AgentList)
                {
                    Vector2 screenCoords = Camera.main.WorldToScreenPoint(agent.transform.position);

                    Rect mouseRect = new Rect(Mathf.Min(mouse1stPoint.x, mouse2ndPoint.x), Mathf.Min(mouse1stPoint.y, mouse2ndPoint.y),
                                      Mathf.Abs(mouse1stPoint.x - mouse2ndPoint.x), (Mathf.Abs(mouse1stPoint.y - mouse2ndPoint.y)));

                    if (mouseRect.Contains(screenCoords))
                    {
                        agent.GetComponent<NavMeshAgent>().Resume();
                        agent.GetComponent<AgentScript>().unStop();
                        agent.GetComponent<NavMeshAgent>().SetDestination(agent.transform.position);
                        selectedList.Add(agent);
                        somethingSelected = true;
                    }
                }

            }



            if (Input.GetMouseButtonUp(0))
            {

                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Agent" && hold == true)
                {
                    hit.transform.gameObject.GetComponent<NavMeshAgent>().Resume();
                    hit.transform.gameObject.GetComponent<AgentScript>().unStop();
                    hit.transform.gameObject.GetComponent<NavMeshAgent>().SetDestination(hit.transform.position);
                    selectedList.Add(hit.transform.gameObject);
                    somethingSelected = true;


                }
                else if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Barrier")
                {
                    somethingSelected = true;
                    selectedList.Clear();
                    selectedList.Add(hit.transform.gameObject);
                    rend = hit.transform.gameObject.GetComponent<Renderer>();
                    rend.material.color = Color.gray;
                    barrierSelected = true;
                }
            }


            if (selectedList != null && barrierSelected == false)
            {
                foreach (GameObject agent in selectedList)
                {
                    rend = agent.GetComponent<Renderer>();
                    rend.material.color = Color.red;
                }
            }

            hold = false;
        }

    }
}
