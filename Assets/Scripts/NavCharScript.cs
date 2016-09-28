using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavCharScript : MonoBehaviour
{

    public NavMeshAgent agent;
    
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*
            if (agent != null )
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    agent.GetComponentInParent<Selected>().select = false;
                    Camera.main.GetComponent<Director>().clearAgents(); 
                }      
            }
            */
              
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.GetComponent<NavMeshAgent>() == null)
                {
                    //List<NavMeshAgent> capsules = new List<NavMeshAgent>();
                    GameObject[] caps = GameObject.FindGameObjectsWithTag("Capsule");
                    //foreach (GameObject c in caps)
                    //{
                    //    capsules.Add(c.GetComponent<NavMeshAgent>()); 
                    //}

                    //foreach (NavMeshAgent a in Camera.main.GetComponent<Director>().agents)

                    foreach (GameObject c in caps)
                    {
                        //a.GetComponent<Selected>().select = true;
                        if (c.GetComponent<Selected>().select == true)
                        {
                            c.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                        }
                            
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            Debug.Log("add");
                            agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                            agent.GetComponent<Selected>().select = true; 
                            //hit.collider.gameObject.GetComponent<Selected>().select = true;
                            Camera.main.GetComponent<Director>().addAgent();
                        }
                        else
                        {
                            Debug.Log("new");
                            agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                            //hit.collider.gameObject.GetComponent<Selected>().select = true;
                            Camera.main.GetComponent<Director>().clearAgents();
                            Camera.main.GetComponent<Director>().addAgent();
                        }

                    }
                }
                
                
            }
        }


    }
}
