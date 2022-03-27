using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerCompleteMaze : MonoBehaviour
{
    public AudioSource rewardSound;
    private Collider thisCollider;

    private void Start()
    {
        thisCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rewardSound.Play();
            thisCollider.enabled = false;
        }
    }
}
