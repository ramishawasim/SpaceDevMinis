using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class CollectableOnTriggerEnter : MonoBehaviour
{
    private Text collectableCounter;
    public int collectableNumber;
    private int collectable;

    public GameObject inventory;
    private Vector3 startingPosition;
    private GameObject renderedEgg;
    private GameObject renderedLight;
    private GameObject particleSystem1;
    private GameObject particleSystem2;

    private Inventory inventoryComponent;

    private void Awake()
    {
        if (collectableNumber == 1)
        {
            GameObject counterRef = GameObject.Find("Collectable1Counter");
            collectableCounter = counterRef.GetComponent<Text>();
        }

        if (collectableNumber == 2)
        {
            GameObject counterRef = GameObject.Find("Collectable2Counter");
            collectableCounter = counterRef.GetComponent<Text>();
        }

        if (collectableNumber == 3)
        {
            GameObject counterRef = GameObject.Find("Collectable3Counter");
            collectableCounter = counterRef.GetComponent<Text>();
        }

        startingPosition = gameObject.transform.position;

        inventory = GameObject.Find("Inventory");
        renderedEgg = gameObject.transform.GetChild(0).gameObject;
        renderedLight = renderedEgg.transform.GetChild(0).gameObject;
        particleSystem1 = renderedEgg.transform.GetChild(1).gameObject;
        particleSystem2 = renderedEgg.transform.GetChild(2).gameObject;
        inventoryComponent = inventory.GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider hit)
    {
        // This is the hit event
        if (hit.tag == "Player")
        {

            // Update player inventory
            if (collectableNumber == 1)
            {
                inventoryComponent.collectable1Counter++;
            } else if (collectableNumber == 2)
            {
                inventoryComponent.collectable2Counter++;
            } else if (collectableNumber == 3) {
                inventoryComponent.collectable3Counter++;
            }

            // The script doesn't know the collectable count without this
            collectable = int.Parse (collectableCounter.text);
            collectable++;

            // Collectable Events
            inventoryComponent.collectableEvents();

            // Disabling/Enabling stuff
            gameObject.GetComponent<SphereCollider>().enabled = false;
            renderedEgg.GetComponent<MeshRenderer>().enabled = false;
            renderedLight.SetActive(false);
            particleSystem1.GetComponent<VisualEffect>().Stop();
            particleSystem2.SetActive(true);


            UpdateGUI();
        }
    }
    private void UpdateGUI()
    {
        collectableCounter.text = collectable.ToString();
    }
}
