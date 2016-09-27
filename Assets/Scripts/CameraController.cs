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
            transform.position += this.transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= this.transform.right* speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= this.transform.forward * speed * Time.deltaTime;
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
            transform.position += this.transform.forward * speed*15 * Time.deltaTime;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            transform.position -= this.transform.forward * speed *15 * Time.deltaTime;
        }


    }
}
