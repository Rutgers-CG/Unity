using UnityEngine;
using System.Collections;

public class SelfMovement : MonoBehaviour
{
    public float freq = 1f;
    public float amplitude = 1f;
    Vector3 moveObject;
    float time = 0f;
        
    // Use this for initialization
        void Start()
        {
            moveObject = transform.position;
        }

   // Update is called once per frame
        void Update()
        {

            time = time + Time.deltaTime * Time.timeScale * freq;
            transform.position = moveObject + Vector3.up * Mathf.Abs(Mathf.Sin(time)) * amplitude;
            

        }
    }
