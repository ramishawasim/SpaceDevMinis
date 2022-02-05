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

    public GameObject[] campfires;

    PlayerAndAnimationControllerV2 playerAndAnimationControllerV2;

    public bool isCurrentRespawn = false;

    void Start()
    {
        player = GameObject.Find("Player");
        thisCampfireLocation = this.transform.position;
        thisCampfireLocation.z = thisCampfireLocation.z - 1.5f;

        fireParticles = this.transform.GetChild(1).gameObject;
        fireLight = this.transform.GetChild(2).gameObject;
        smokeParticles = this.transform.GetChild(3).gameObject;

        playerAndAnimationControllerV2 = player.GetComponent<PlayerAndAnimationControllerV2>();
        campfires = GameObject.FindGameObjectsWithTag("Campfire");

    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {

            for (int i = 0; i < campfires.Length; i++)
            {
                CampfireRespawn campfireRespawn = campfires[i].GetComponent<CampfireRespawn>();
                campfireRespawn.isCurrentRespawn = false;
            }

            isCurrentRespawn = true;

            for (int i = 0; i < campfires.Length; i++)
            {
                CampfireRespawn campfireRespawn = campfires[i].GetComponent<CampfireRespawn>();
                campfireRespawn.noLongerRespawn();
            }

            playerAndAnimationControllerV2.setRespawnLocation(thisCampfireLocation);
            fireParticles.SetActive(true);
            fireLight.SetActive(true);
            smokeParticles.SetActive(true);
        }
    }

    public void noLongerRespawn()
    {
        if (!isCurrentRespawn)
        {
            isCurrentRespawn = false;
            fireParticles.SetActive(false);
            fireLight.SetActive(false);
            smokeParticles.SetActive(false);
        }
    }
}
