using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavCharScript : MonoBehaviour
{

    public NavMeshAgent agent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {              
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.GetComponent<NavMeshAgent>() == null)
                {
                    GameObject[] caps = GameObject.FindGameObjectsWithTag("Capsule");

                    foreach (GameObject c in caps)
                    {
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
                            Camera.main.GetComponent<Director>().addAgent();
                        }
                        else
                        {
                            Debug.Log("new");
                            agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                            Camera.main.GetComponent<Director>().clearAgents();
                            agent.GetComponent<Selected>().select = true;
                            Camera.main.GetComponent<Director>().addAgent();
                        }

                    }
                }
                
                
            }
        }


    }
}
