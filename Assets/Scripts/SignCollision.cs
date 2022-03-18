using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCollision : MonoBehaviour
{

    public GameObject DialogueBox;
    public GameObject InteractPrompt;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InteractPrompt.SetActive(true);
            Debug.Log("CAN INTERACT WITH SIGN");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InteractPrompt.SetActive(false);
            Debug.Log("CANNOT INTERACT WITH SIGN");
        }
    }





}
