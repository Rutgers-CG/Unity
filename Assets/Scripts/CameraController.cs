using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Enum Variables

    public enum KEYBOARD_CONTROLS : int
    {
        FOWARD = KeyCode.W,
        BACKWARD = KeyCode.S,
        LEFT_STRAFE = KeyCode.A,
        RIGHT_STRAFE = KeyCode.D,
        SPEED_MULTIPLY = KeyCode.LeftShift
    }

    public enum MOUSE_CONTROLS : int
    {
        PITCH_MOUSE = KeyCode.Mouse1,
        YAW_MOUSE = KeyCode.Mouse1
    }

    #endregion

    #region Variables

    private float speedMultiplier = 4f;
    [Range(1f, 20f)]public float objectRotationSpeed = 5f;
    [Range(1f, 20f)] public float cameraRotationThreshold= 0.5f;
    [Tooltip("Inverts the vertical axis on camera.")]public bool invertVertical = true;
    [Tooltip("Inverts the horizontal axis on camera.")] public bool invertHorizontal = false;

    private Transform mainCamera;

    private Vector2 lastMousePosition;

    #endregion

    #region CameraController Functions

    public void Start ()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey((KeyCode) KEYBOARD_CONTROLS.SPEED_MULTIPLY))
        {
           speedMultiplier = 8f;
        } else {
           speedMultiplier = 4f;
        }

        //Handling the keyboard input : translations
        foreach (KEYBOARD_CONTROLS key in Enum.GetValues(typeof(KEYBOARD_CONTROLS)))
        {
            if(Input.GetKey((KeyCode) key))
            {
                switch(key)
                {
                    // translation movements
                    case KEYBOARD_CONTROLS.FOWARD:
                        mainCamera.position += (mainCamera.forward * Time.deltaTime * speedMultiplier);
                        break;
                    case KEYBOARD_CONTROLS.BACKWARD:
                        mainCamera.position -= (mainCamera.forward * Time.deltaTime * speedMultiplier);
                        break;
                    case KEYBOARD_CONTROLS.LEFT_STRAFE:
                        mainCamera.position -= (mainCamera.right * Time.deltaTime * speedMultiplier);
                        break;
                    case KEYBOARD_CONTROLS.RIGHT_STRAFE:
                        mainCamera.position += (mainCamera.right * Time.deltaTime * speedMultiplier);
                        break;
                }
            }
        }

        //Handling the mouse input : rotations
        foreach (MOUSE_CONTROLS button in Enum.GetValues(typeof(MOUSE_CONTROLS)))
        {
            if (Input.GetKey((KeyCode)button))
            {
                float horizontalDelta = lastMousePosition.x - Input.mousePosition.x;
                float verticalDelta = lastMousePosition.y - Input.mousePosition.y;

                // rotations : rotate horizontally then vertically
                if(Mathf.Abs(horizontalDelta) > cameraRotationThreshold)
                {
                    if (invertHorizontal)
                    {
                        horizontalDelta *= -1;
                    }

                    mainCamera.Rotate(Vector3.up, horizontalDelta * speedMultiplier * objectRotationSpeed * Time.deltaTime, Space.World);
                }

                if (Mathf.Abs(verticalDelta) > cameraRotationThreshold)
                {
                    if (invertVertical)
                    {
                        verticalDelta *= -1;
                    }

                    mainCamera.Rotate(mainCamera.right, verticalDelta * speedMultiplier * objectRotationSpeed * Time.deltaTime, Space.World);
                }

            }
        }

        lastMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void ChangeVerticalInvert()
    {
        invertVertical = !invertVertical;
    }

    public void ChangeHorizontalInvert()
    {
        invertHorizontal = !invertHorizontal;
    }

    #endregion
}
