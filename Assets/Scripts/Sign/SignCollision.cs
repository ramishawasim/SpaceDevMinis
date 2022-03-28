using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignCollision : MonoBehaviour
{

    // You need to set the Dialogue Box in the editor. The interact prompt is something that needs to be found in the scene. If it's a child of a prefab there's a bit more that needs to be done.
    public GameObject DialogueBox;
    public GameObject InteractPrompt;
    private bool inRange;
    PauseAction action;


    private void Awake(){
        action = new PauseAction();
    }

      private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }


    private void Start()
    {
       // InteractPrompt = GameObject.Find("InteractPrompt");
      
       action.Interaction.InteractionAction.performed += _ => DetermineInteraction();
    }
      private void DetermineInteraction()
    {
        if (inRange)
        {
            if(DialogueBox.activeInHierarchy){
                DialogueBox.SetActive(false);
            }
            else{
                DialogueBox.SetActive(true);
            }
        }
        
        else{
          //  DialogueBox.SetActive(false);

        }
       
    }

    




    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            Debug.Log("CAN INTERACT WITH SIGN");
            inRange = true;
            InteractPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player")
        {
            Debug.Log("CANNOT INTERACT WITH SIGN");
            inRange = false;
            InteractPrompt.SetActive(false);
            DialogueBox.SetActive(false);
        }
    }






}
