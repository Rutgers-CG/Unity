using System;
using UnityEngine;

public class MovableObstacleScript : MonoBehaviour
{
    public enum KEYBOARD_CONTROLS : int
    {
        FOWARD = KeyCode.UpArrow,
        BACKWARD = KeyCode.DownArrow,
        LEFT_STRAFE = KeyCode.LeftArrow,
        RIGHT_STRAFE = KeyCode.RightArrow,
    }

    #region Variables

    public float speed = 3f;

    public bool isDoor = false;
    private bool isSelected = false;

    public GameObject obstacle;

    public Material[] materials;
    private Renderer rend;

    #endregion

    #region MovableObstacle Functions

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform == this.transform)
                {
                    if (this.isSelected == false)
                    {
                        this.isSelected = true;
                        rend.sharedMaterial = materials[1];
                    }
                    else if (this.isSelected == true)
                    {
                        this.isSelected = false;
                        rend.sharedMaterial = materials[0];
                    }
                }
            }
        }

        foreach (KEYBOARD_CONTROLS key in Enum.GetValues(typeof(KEYBOARD_CONTROLS)))
        {
            if (Input.GetKey((KeyCode)key) && this.isSelected == true)
            {
                switch (key)
                {
                    // translation movements
                    case KEYBOARD_CONTROLS.FOWARD:
                        if (isDoor)
                        {
                            obstacle.transform.position += new Vector3(0, Time.deltaTime * speed, 0);
                            break;
                        }
                        obstacle.transform.position -= new Vector3(0, 0, Time.deltaTime * speed);
                        break;
                    case KEYBOARD_CONTROLS.BACKWARD:
                        if (isDoor)
                        {
                            obstacle.transform.position -= new Vector3(0, Time.deltaTime * speed, 0);
                            break;
                        }
                        obstacle.transform.position += new Vector3(0, 0, Time.deltaTime * speed);
                        break;
                    case KEYBOARD_CONTROLS.LEFT_STRAFE:
                        if (isDoor)
                        {
                            obstacle.transform.position += new Vector3(0, 0, Time.deltaTime * speed);
                            break;
                        }
                        obstacle.transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
                        break;
                    case KEYBOARD_CONTROLS.RIGHT_STRAFE:
                        if (isDoor)
                        {
                            obstacle.transform.position -= new Vector3(0, 0, Time.deltaTime * speed);
                            break;
                        }
                        obstacle.transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
                        break;
                }
            }
        }
    }

    #endregion
}
