using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; 

public class FreeLook : MonoBehaviour {

    public float speed;
    public float rotateSpeed; 

	// Update is called once per frame
	void LateUpdate () {

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                this.transform.Rotate(rotateSpeed * Vector3.up * Time.deltaTime, Space.World);
            }
            else
            {
                this.transform.Translate(new Vector3(speed, 0, 0));
            }   
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                this.transform.Rotate(-1 * rotateSpeed * Vector3.up * Time.deltaTime, Space.World);
            }
            else
            {
                this.transform.Translate(new Vector3(-1 * speed, 0, 0));
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(new Vector3(0, 0, speed));
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(new Vector3(0, 0, -1 * speed));
        }


    }
}
