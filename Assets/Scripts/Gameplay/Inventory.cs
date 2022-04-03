using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public int collectable1Counter = 0;
    public int collectable2Counter = 0;
    public int collectable3Counter = 0;

    public GameObject goldAwardEgg;
    public GameObject blueAwardEgg;

    public void collectableEvents()
    {
        if (collectable1Counter == 350)
        {
            Debug.Log("300 GOLD COLLECTABLES");
            goldAwardEgg.SetActive(true);
        }

        if (collectable2Counter == 40)
        {
            Debug.Log("50 BLUE COLLECTABLES");
            blueAwardEgg.SetActive(true);
        }

        if (collectable3Counter == 5)
        {
            Debug.Log("5 PURPLE COLLECTABLES");
            SceneManager.LoadScene(9);
        }
    }
}
