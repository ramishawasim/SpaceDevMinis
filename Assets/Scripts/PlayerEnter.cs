using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            gameObject.GetComponent<FloatingBall>().isFloating = false;
            gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
        }
    }
}
