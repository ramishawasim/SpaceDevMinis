using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableOnTriggerEnter : MonoBehaviour
{
    private Text collectableCounter;
    public int collectableNumber;
    private int collectable;

    public GameObject inventory;
    private Vector3 startingPosition;

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
    }

    private void OnTriggerEnter(Collider hit)
    {
        // This is the hit event
        if (hit.tag == "Player")
        {

            // Update player inventory
            if (collectableNumber == 1)
            {
                inventory.GetComponent<Inventory>().collectable1Counter++;
            } else if (collectableNumber == 2)
            {
                inventory.GetComponent<Inventory>().collectable2Counter++;
            } else if (collectableNumber == 3) {
                inventory.GetComponent<Inventory>().collectable3Counter++;
            }

            // The script doesn't know the collectable count without this
            collectable = int.Parse (collectableCounter.text);
            collectable++;

            // Collectable Events
            if (inventory.GetComponent<Inventory>().collectable1Counter == 50)
            {
                Debug.Log("50 GOLD COLLECTABLES");
            }
            Destroy(gameObject);
            UpdateGUI();
        }
    }
    private void UpdateGUI()
    {
        collectableCounter.text = collectable.ToString();
    }
}
