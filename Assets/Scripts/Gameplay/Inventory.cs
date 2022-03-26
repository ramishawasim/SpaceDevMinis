using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int collectable1Counter = 0;
    public int collectable2Counter = 0;
    public int collectable3Counter = 0;

    public void collectableEvents()
    {
        if (collectable1Counter == 300)
        {
            Debug.Log("300 GOLD COLLECTABLES");
        }

        if (collectable2Counter == 50)
        {
            Debug.Log("50 BLUE COLLECTABLES");
        }

        if (collectable3Counter == 5)
        {
            Debug.Log("5 PURPLE COLLECTABLES");
        }
    }
}
