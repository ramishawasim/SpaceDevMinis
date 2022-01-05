using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1OnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PushCollider")
        {
            Debug.Log("Platform Success");
        }
    }
}
