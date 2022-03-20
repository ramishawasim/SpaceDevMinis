using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Platform1OnTriggerEnter : MonoBehaviour
{
    private GameObject pushCollider;
    private MeshRenderer BallMeshRenderer;
    private GameObject PointLight;
    private MeshRenderer InnerBallMeshRenderer;
    private InnerMarbleMadness SpinningInnerBall;
    private VisualEffect TrailVFX;
    private GameObject killTruncatedIcosahedron;

    private void Start()
    {
        pushCollider = GameObject.FindGameObjectWithTag("PushCollider");
        BallMeshRenderer = pushCollider.GetComponent<MeshRenderer>();
        PointLight = pushCollider.transform.GetChild(0).gameObject;
        InnerBallMeshRenderer = pushCollider.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();
        SpinningInnerBall = pushCollider.transform.GetChild(1).gameObject.GetComponent<InnerMarbleMadness>();
        TrailVFX = pushCollider.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<VisualEffect>();
        killTruncatedIcosahedron = pushCollider.transform.GetChild(2).gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PushCollider")
        {
            Debug.Log("Platform Success");

            pushCollider.GetComponent<Rigidbody>().isKinematic = false;
            //pushCollider.GetComponent<SphereCollider>().enabled = false;

            BallMeshRenderer.enabled = false;
            PointLight.SetActive(false);
            InnerBallMeshRenderer.enabled = false;
            SpinningInnerBall.enabled = false;
            TrailVFX.Stop();
            killTruncatedIcosahedron.SetActive(true);            
        }
    }
}
