using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SnowVFXSpawn : MonoBehaviour
{
    public GameObject snowVFXVolume;
    public VisualEffect snowVFXComponent;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            snowVFXComponent.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            snowVFXComponent.Stop();
        }
    }
}
