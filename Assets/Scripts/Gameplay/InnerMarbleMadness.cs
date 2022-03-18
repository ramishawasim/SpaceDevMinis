using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attach to follower 
public class InnerMarbleMadness : MonoBehaviour
{
    public GameObject followedObject;
    public float degreesPerSecond = 180f;

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        // transform.position = followedObject.transform.position;
    }
}
