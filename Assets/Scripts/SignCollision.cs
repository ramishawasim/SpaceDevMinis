using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCollision : MonoBehaviour
{

    // You need to set the Dialogue Box in the editor. The interact prompt is something that needs to be found in the scene. If it's a child of a prefab there's a bit more that needs to be done.
    public GameObject DialogueBox;
    public GameObject InteractPrompt;


    private void Start()
    {
        InteractPrompt = GameObject.Find("InteractPrompt");
    }



    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            Debug.Log("CAN INTERACT WITH SIGN");
            //InteractPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player")
        {
            Debug.Log("CANNOT INTERACT WITH SIGN");
            //InteractPrompt.SetActive(false);
        }
    }





}
