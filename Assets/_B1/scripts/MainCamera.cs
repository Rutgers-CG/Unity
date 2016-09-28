using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour {

    public float speed=5.0f;
    public Transform target;

    public static bool BarrierSelected;




    // Use this for initialization
    void Start () {

        BarrierSelected = false;


    }
	
	// Update is called once per frame
	void Update () {

        if (BarrierSelected) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

        float x = 0 , y = 0;


        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * speed;
            y += Input.GetAxis("Mouse Y") * speed * .2f;

            transform.RotateAround(target.position, transform.up, x);
            transform.RotateAround(target.position, transform.right, -y);

        }

        float zoomDist = Vector3.Distance(transform.position, target.position);

        zoomDist = zoomDist - Input.GetAxis("Mouse ScrollWheel") * speed;
        transform.position  = -transform.forward*zoomDist + target.position;



    }
}
