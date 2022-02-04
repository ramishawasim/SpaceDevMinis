using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireRespawn : MonoBehaviour
{
    GameObject player;
    Vector3 thisCampfireLocation;
    GameObject fireParticles;
    GameObject fireLight;
    GameObject smokeParticles;
    // should probably store the CLASS and not get the component but that's none of my business

    void Start()
    {
        player = GameObject.Find("Player");
        thisCampfireLocation = this.transform.position;
        thisCampfireLocation.z = thisCampfireLocation.z - 1.5f;

        fireParticles = this.transform.GetChild(1).gameObject;
        fireLight = this.transform.GetChild(2).gameObject;
        smokeParticles = this.transform.GetChild(3).gameObject;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            player.GetComponent<PlayerAndAnimationControllerV2>().setRespawnLocation(thisCampfireLocation);
            fireParticles.SetActive(true);
            fireLight.SetActive(true);
            smokeParticles.SetActive(true);
        }
    }
}
