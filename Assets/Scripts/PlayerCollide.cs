using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Attach to Player
//Player Reset functionality
public class PlayerCollide : MonoBehaviour
{
    public float threshold = -50f;

    public Text collectable1Counter;
    public Text collectable2Counter;

    private int collectable1 = 0;
    private int collectable2 = 0;

    void Update()
    {
        if (transform.position.y < threshold)
        {
            reset();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Collectable1")
        {
            Debug.Log("Collectable1 Collected");
            collectable1++;
            Destroy(hit.gameObject);
            UpdateGUI();
        }

        else if (hit.gameObject.tag == "Collectable2")
        {
            Debug.Log("Collectable2 Collected");
            collectable2++;
            Destroy(hit.gameObject);
            UpdateGUI();
        }
    }

    private void UpdateGUI()
    {
        collectable1Counter.text = collectable1.ToString();
        collectable2Counter.text = collectable2.ToString();
    }

    void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
