using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    private GameObject player;
    private Renderer ChocoRenderer;
    private AudioSource burnSound;
    private PlayerAndAnimationControllerV2 playerAndAnimationControllerV2;

    private void Start()
    {
        player = GameObject.Find("Player");
        burnSound = player.transform.GetChild(5).gameObject.GetComponent<AudioSource>();
        ChocoRenderer = player.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        playerAndAnimationControllerV2 = player.GetComponent<PlayerAndAnimationControllerV2>();
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            playerAndAnimationControllerV2.onDeath();
            burnSound.Play();
            StartCoroutine(DissolvePlayer());
        }
    }

    IEnumerator DissolvePlayer()
    {
        for (float dissolve = 0f; dissolve < 1; dissolve += 0.01f)
        {
            ChocoRenderer.material.SetFloat("_DISSOLVE", dissolve);
            Debug.Log("fadein" + dissolve);
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitUntil(() => !playerAndAnimationControllerV2.isDead);
        ChocoRenderer.material.SetFloat("_DISSOLVE", 0f);
    }
}
