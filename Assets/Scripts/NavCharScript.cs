using UnityEngine;
using System.Collections;

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
            if (agent != null )
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    agent.GetComponentInParent<Selected>().select = false;
                    Camera.main.GetComponent<Director>().clearAgents(); 
                }      
            }
                
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                        //hit.collider.gameObject.GetComponent<Selected>().select = true;
                        Camera.main.GetComponent<Director>().addAgent();
                    }
                    else
                    {
                        agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                        //hit.collider.gameObject.GetComponent<Selected>().select = true;
                        Camera.main.GetComponent<Director>().clearAgents();
                        Camera.main.GetComponent<Director>().addAgent();
                    }
                    
                }
                foreach(NavMeshAgent a in Camera.main.GetComponent<Director>().agents) 
                {
                    a.GetComponent<Selected>().select = true;
                    a.SetDestination(hit.point);
                }
                
            }
        }


    }
}
