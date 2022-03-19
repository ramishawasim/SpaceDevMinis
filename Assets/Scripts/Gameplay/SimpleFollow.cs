using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public GameObject followedObject;

    // Update is called once per frame
    void Update()
    {
        transform.position = followedObject.transform.position;
    }
}
