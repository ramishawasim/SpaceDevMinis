using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableOnTriggerEnter : MonoBehaviour
{
    private Text collectableCounter;
    public int collectableNumber;
    private int collectable = 0;

    private GameObject blockade1;

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

        blockade1 = GameObject.Find("Blockade1");
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            collectable = int.Parse (collectableCounter.text);
            collectable++;
            if (collectable == 50)
            {
                Destroy(blockade1);
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
