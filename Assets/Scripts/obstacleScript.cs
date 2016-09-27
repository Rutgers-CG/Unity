using UnityEngine;
using System.Collections;

public class obstacleScript : MonoBehaviour {
    public bool isSelected;
    public string id;
	// Use this for initialization
	void Start () {
	    
	}

    // Update is called once per frame
    void Update()
    {

        if (isSelected)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.green;
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(moveHorizontal / 3, 0.0f, moveVertical / 3));
        }
        else
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.yellow;
        }
    }
}
