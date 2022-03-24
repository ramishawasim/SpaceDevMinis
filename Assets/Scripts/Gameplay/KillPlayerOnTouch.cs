using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    private GameObject player;
    public Renderer ChocoRenderer;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            player.GetComponent<PlayerAndAnimationControllerV2>().onDeath();
            StartCoroutine(DissolvePlayer());
        }
    }

    IEnumerator DissolvePlayer()
    {
        for (float dissolve = 0f; dissolve < 1; dissolve += 0.01f)
        {
            ChocoRenderer.material.SetFloat("_DISSOLVE", dissolve);
            Debug.Log("fadein" + dissolve);
            yield return new WaitForSeconds(.0175f);
        }

        ChocoRenderer.material.SetFloat("_DISSOLVE", 0f);
    }
}
