using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attach to Player
//Player Reset functionality
public class PlayerReset : MonoBehaviour
{
    public float threshold = -10f;

    void Update()
    {
        if (transform.position.y < threshold)
        {
            reset();
        }
    }

    void reset()
    {
        this.GetComponent<PlayerAndAnimationControllerV2>().onDeath();
    }
}
