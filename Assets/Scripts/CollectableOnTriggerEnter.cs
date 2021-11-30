using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableOnTriggerEnter : MonoBehaviour
{
    public Text collectableCounter;
    public int collectableNumber;
    private int collectable = 0;

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
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            Debug.Log("Collectable Collected");
            collectable = int.Parse (collectableCounter.text);
            collectable++;
            Destroy(gameObject);
            UpdateGUI();
        }
    }
    private void UpdateGUI()
    {
        collectableCounter.text = collectable.ToString();
    }
}
