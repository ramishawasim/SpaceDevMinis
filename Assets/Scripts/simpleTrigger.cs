using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleTrigger : MonoBehaviour
{
    public GameObject objectToEnableDisable;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            objectToEnableDisable.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            objectToEnableDisable.SetActive(false);
        }
    }
}
