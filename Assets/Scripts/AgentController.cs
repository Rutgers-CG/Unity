using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    #region Variables
    private NavMeshAgent agent;

    private bool isSelected = false;

    public Material[] materials;
    private Renderer rend;
    #endregion

    #region AgentController Functions
    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.isSelected == true)
        {
            rend.sharedMaterial = materials[1];
        }
        else if (this.isSelected == false)
        {
            rend.sharedMaterial = materials[0];
        }
    }

    public bool GetSelectionStatus()
    {
        return isSelected;
    }

    public void SetSelectionStatus(bool status)
    {
        isSelected = status;
    }

    public void GoTo(Vector3 newDestination)
    {
        if (this.isSelected)
        {
            agent.destination = newDestination;
            agent.isStopped = false;
        }
    }
    #endregion
}
