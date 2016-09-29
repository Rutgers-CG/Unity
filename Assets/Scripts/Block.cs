using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public float speed;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.gameObject.transform.Translate(speed * Vector3.forward);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.gameObject.transform.Translate(speed * Vector3.back);
        }
    }
}
