using UnityEngine;

public class DirectorScript : MonoBehaviour
{
    #region Variables
    private AgentController[] agents;

    private Ray shootRay;
    private RaycastHit shootHit;
    #endregion

    #region DirectorScript Functions
    // Update is called once per frame
    void Update ()
    {
        UpdateAgents();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (!hit.collider.CompareTag("Agent"))
                {
                    foreach (AgentController a in agents)
                    {
                        a.GoTo(hit.point);
                    }
                }

                foreach (AgentController a in agents)
                {
                    if(hit.transform == a.transform)
                    {
                        if (!a.GetSelectionStatus())
                        {
                            a.SetSelectionStatus(true);
                        } else if (a.GetSelectionStatus())
                        {
                            a.SetSelectionStatus(false);
                        }
                    }
                }
            }
        }
    }

    void UpdateAgents()
    {
        agents = FindObjectsOfType<AgentController>();
    }
    #endregion
}
