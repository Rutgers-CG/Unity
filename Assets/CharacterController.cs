using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float zForce = Input.GetAxis("Vertical");
        float xForce = Input.GetAxis("Horizontal");
        anim.SetFloat("zForce", zForce);
        anim.SetFloat("xForce", xForce);
        if(zForce == 0 && xForce!=0)
        {
            transform.Rotate(new Vector3(0, xForce * 60 * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }

    }
}
