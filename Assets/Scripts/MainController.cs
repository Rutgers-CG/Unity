using System;
using UnityEngine;

public class MainController : MonoBehaviour {

    #region Enums

    public enum TOGGLE_MODE : int {
        TARGET_OBJECT = KeyCode.F1,
        TARGET_CAMERA = KeyCode.F2
    }

    public enum KEYBOARD_INPUT : int {
        SPEED_MODIFIER = KeyCode.LeftShift,
        SPACE_MODIFIER = KeyCode.F3,
        YAW_POS = KeyCode.D,
        YAW_NEG = KeyCode.A,
        PITCH_POS = KeyCode.W,
        PITCH_NEG = KeyCode.S,
        ROLL_POS = KeyCode.Q,
        ROLL_NEG = KeyCode.E,
        FORWARD = KeyCode.W,
        BACKWARDS = KeyCode.S,
        LEFT_STRAFE = KeyCode.A,
        RIGHT_STRAFE = KeyCode.D,
        LEFT_ROTATE = KeyCode.Q,
        RIGHT_ROTATE = KeyCode.E
    }

    public enum MOUSE_INPUT : int {
        PITCH_MOUSE = KeyCode.Mouse1,
        YAW_MOUSE = KeyCode.Mouse1
    }

    #endregion

    #region Members

    public Transform TargetObject;
    [Range(1f, 20f)] public float ObjectRotationSpeed = 5f;
    [Range(1f, 20f)] public float CameraRotationThreshold = 0.5f;
    public bool InvertCameraAxis = false;
    public TOGGLE_MODE ToggleMode = TOGGLE_MODE.TARGET_CAMERA;
    
    private Transform g_Camera;

    private Space g_SpaceTarget = Space.World;

    private Vector2 g_LastMousePosition;

    #endregion

    #region Unity_Functions

    public void Start() {
        g_Camera = Camera.main.transform;
    }
    
    // Update is called once per frame
    void Update () {

        float speedMod = Input.GetKey((KeyCode)KEYBOARD_INPUT.SPEED_MODIFIER) ? 2f : 1f;

        if(Input.GetKeyDown((KeyCode)KEYBOARD_INPUT.SPACE_MODIFIER)) {
            if (g_SpaceTarget == Space.World) {
                g_SpaceTarget = Space.Self;
                Debug.Log("Space::Self");
            } else {
                g_SpaceTarget = Space.World;
                Debug.Log("Space::World");
            }
        }

        if (Input.GetKeyDown( (KeyCode) TOGGLE_MODE.TARGET_OBJECT) ) {
            ToggleMode = TOGGLE_MODE.TARGET_OBJECT;
            Debug.Log("Object Selected");
        } else if (Input.GetKeyDown((KeyCode)TOGGLE_MODE.TARGET_CAMERA)) {
            ToggleMode = TOGGLE_MODE.TARGET_CAMERA;
            Debug.Log("Camera Selected");
        }

        // Hanlde Mouse

        foreach(MOUSE_INPUT mi in Enum.GetValues(typeof(MOUSE_INPUT))) {
            if(Input.GetKey((KeyCode)mi)) {
                float horDelta = g_LastMousePosition.x - Input.mousePosition.x;
                float verDelta = g_LastMousePosition.y - Input.mousePosition.y;
                if (Mathf.Abs(horDelta) > CameraRotationThreshold) {
                    int horDir = horDelta > 0 ? (InvertCameraAxis ? 1 : -1) : (InvertCameraAxis ? -1 : 1);
                    g_Camera.Rotate(Vector3.up, horDir * speedMod * ObjectRotationSpeed * Time.deltaTime, Space.World);
                }
                if (Mathf.Abs(verDelta) > CameraRotationThreshold) {
                    int verDir = verDelta > 0 ? (InvertCameraAxis ? -1 : 1) : (InvertCameraAxis ? 1 : -1);
                    g_Camera.Rotate(g_Camera.right, verDir * speedMod * ObjectRotationSpeed * Time.deltaTime, Space.World);
                }

            }
        }


        // Handle Keyboard

        if(ToggleMode == TOGGLE_MODE.TARGET_OBJECT && TargetObject != null) {
            foreach (KEYBOARD_INPUT val in Enum.GetValues(typeof(KEYBOARD_INPUT))) {
                if(Input.GetKey((KeyCode) val)) {
                    switch(val) {
                        case KEYBOARD_INPUT.YAW_POS:
                            switch(g_SpaceTarget) {
                                case Space.World:
                                    TargetObject.RotateAround(Vector3.zero, Vector3.up, speedMod * ObjectRotationSpeed * Time.deltaTime);
                                    break;
                                case Space.Self:
                                    TargetObject.Rotate(TargetObject.up, speedMod * ObjectRotationSpeed * Time.deltaTime, Space.World);
                                    break;
                            }
                            break;
                        case KEYBOARD_INPUT.YAW_NEG:
                            switch (g_SpaceTarget) {
                                case Space.World:
                                    TargetObject.RotateAround(Vector3.zero, Vector3.up, -(speedMod * ObjectRotationSpeed * Time.deltaTime));
                                    break;
                                case Space.Self:
                                    TargetObject.Rotate(TargetObject.up, -(speedMod * ObjectRotationSpeed * Time.deltaTime), Space.World);
                                    break;
                            }
                            break;
                        case KEYBOARD_INPUT.PITCH_POS:
                            switch (g_SpaceTarget) {
                                case Space.World:
                                    TargetObject.RotateAround(Vector3.zero, Vector3.right, speedMod * ObjectRotationSpeed * Time.deltaTime);
                                    break;
                                case Space.Self:
                                    TargetObject.Rotate(TargetObject.right, speedMod * ObjectRotationSpeed * Time.deltaTime, Space.World);
                                    break;
                            }
                            break;
                        case KEYBOARD_INPUT.PITCH_NEG:
                            switch (g_SpaceTarget) {
                                case Space.World:
                                    TargetObject.RotateAround(Vector3.zero, Vector3.right, -(speedMod * ObjectRotationSpeed * Time.deltaTime));
                                    break;
                                case Space.Self:
                                    TargetObject.Rotate(TargetObject.right, -(speedMod * ObjectRotationSpeed * Time.deltaTime), Space.World);
                                    break;
                            }
                            break;
                        case KEYBOARD_INPUT.ROLL_POS:
                            switch (g_SpaceTarget) {
                                case Space.World:
                                    TargetObject.RotateAround(Vector3.zero, Vector3.forward, speedMod * ObjectRotationSpeed * Time.deltaTime);
                                    break;
                                case Space.Self:
                                    TargetObject.Rotate(TargetObject.forward, speedMod * ObjectRotationSpeed * Time.deltaTime, Space.World);
                                    break;
                            }
                            break;
                        case KEYBOARD_INPUT.ROLL_NEG:
                            switch (g_SpaceTarget) {
                                case Space.World:
                                    TargetObject.RotateAround(Vector3.zero, Vector3.forward, -(speedMod * ObjectRotationSpeed * Time.deltaTime));
                                    break;
                                case Space.Self:
                                    TargetObject.Rotate(TargetObject.forward, -(speedMod * ObjectRotationSpeed * Time.deltaTime), Space.World);
                                    break;
                            }
                            break;
                    }
                }
                
            }
        } else if (ToggleMode == TOGGLE_MODE.TARGET_CAMERA && g_Camera != null) {
            foreach (KEYBOARD_INPUT val in Enum.GetValues(typeof(KEYBOARD_INPUT))) {
                if(Input.GetKey((KeyCode) val)) {
                    switch(val) {
                        case KEYBOARD_INPUT.FORWARD:
                            g_Camera.position += (g_Camera.forward * Time.deltaTime * speedMod);
                            break;
                        case KEYBOARD_INPUT.BACKWARDS:
                            g_Camera.position -= (g_Camera.forward * Time.deltaTime * speedMod);
                            break;
                        case KEYBOARD_INPUT.LEFT_STRAFE:
                            g_Camera.position -= (g_Camera.right * Time.deltaTime * speedMod);
                            break;
                        case KEYBOARD_INPUT.RIGHT_STRAFE:
                            g_Camera.position += (g_Camera.right * Time.deltaTime * speedMod);
                            break;
                        case KEYBOARD_INPUT.LEFT_ROTATE:
                            g_Camera.Rotate(Vector3.up, -(speedMod * Time.deltaTime * ObjectRotationSpeed), Space.World);
                            break;
                        case KEYBOARD_INPUT.RIGHT_ROTATE:
                            g_Camera.Rotate(Vector3.up, speedMod * Time.deltaTime * ObjectRotationSpeed, Space.World);
                            break;
                    }
                }
            }
        }

        // DEBUG INFO
        if (TargetObject != null) {
            if(g_SpaceTarget == Space.World) {
                Debug.DrawRay(TargetObject.position, Vector3.forward, Color.blue);
                Debug.DrawRay(TargetObject.position, Vector3.right, Color.red);
                Debug.DrawRay(TargetObject.position, Vector3.up, Color.green);
            } else {
                Debug.DrawRay(TargetObject.position, TargetObject.forward, Color.blue);
                Debug.DrawRay(TargetObject.position, TargetObject.right, Color.red);
                Debug.DrawRay(TargetObject.position, TargetObject.up, Color.green);
            }
        }

        g_LastMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

    }

    #endregion


}
