using UnityEngine;
using System.Collections;

public class MouseCharacterController : MonoBehaviour {
    Animator anim;

	bool isSelected;
    Vector3 target;
    NavMeshAgent navAgent;
    int jumpNbr = 0;
    // Use this for initialization
    void Start () {

		isSelected = false;
        navAgent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        // monitor the mouse activity, right click on the Goblin will pick the Goblin 
        // and right click on other place will deselect it
		if( Input.GetMouseButtonDown(1)){
			//Debug.Log("Mouse is down");
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "Goblin")
				{
					Debug.Log ("Goblin selected!");
					isSelected = true;
				} else {
					Debug.Log ("de selected!");
                    isSelected = false;
				}
			} else {
				Debug.Log("No hit");
			}
		}

        // monitor the mouse activity, left click to set a target
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (isSelected)
                {
                    target = hit.point;
                    Debug.Log(target);
                    navAgent.SetDestination(target);
					/*float zForce = navAgent.desiredVelocity.z;
					float xForce = navAgent.desiredVelocity.x;
					anim.SetFloat("zForce", zForce);
					anim.SetFloat("xForce", xForce);*/
                }
            }
        }

        // get the off-mesh link, will trigger the jump animation
        // every time it will go through 2 mesh, so use a number to count
        if (navAgent.isOnOffMeshLink) {
            if (jumpNbr % 2 == 0)
            {
                Debug.Log("should jump here!");
                anim.SetTrigger("Jump");
            }
            jumpNbr += 1;
        }

        /*float zForce = Input.GetAxis("Vertical");
        float xForce = Input.GetAxis("Horizontal");
        anim.SetFloat("zForce", zForce);
        anim.SetFloat("xForce", xForce);*/
        
		if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        /*if (Input.GetKey(KeyCode.LeftControl) && xForce!=0)
        {
            anim.SetBool("strafe", true);
        }
        else
        {
            if (zForce == 0 && xForce != 0)
            {
                transform.Rotate(new Vector3(0, xForce * 60 * Time.deltaTime));
            }
            anim.SetBool("strafe", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }*/

    }
}
