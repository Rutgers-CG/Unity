using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    float speed;

	// Use this for initialization
	void Start () {
        speed = 25f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
			Vector3 newPos = new Vector3(0, 0, this.transform.forward.z * speed * Time.deltaTime);
			transform.position += newPos;
        }
        if (Input.GetKey(KeyCode.A))
        {
			transform.position -= this.transform.right* speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
			Vector3 newPos = new Vector3(0, 0, this.transform.forward.z * speed * Time.deltaTime);
			transform.position -= newPos;
        }
        if (Input.GetKey(KeyCode.D))
        {
			transform.position += this.transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            float h = 2 * Input.GetAxis("Mouse X");
            float v = 2 * Input.GetAxis("Mouse Y");
            transform.Rotate(-v, h, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
			Vector3 newPos = new Vector3(0, this.transform.forward.y * speed * 3 * Time.deltaTime, 0);
			transform.position += newPos;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
			Vector3 newPos = new Vector3(0, this.transform.forward.y * speed * 3 * Time.deltaTime, 0);
			transform.position -= newPos;
        }


    }
}
