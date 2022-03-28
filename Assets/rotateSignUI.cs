using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSignUI : MonoBehaviour
{
    private void Start()
    {
        // if statement for checking whether or not the sign has been flipped (Cindy set x scale to -1 to flip the sign).
        // transform.parent gets this object's parent
        // .transform again to get the transform of the parent itself
        // .localScale.x gets the x scale of the game object its attached to 
        if (transform.parent.transform.localScale.x == -1f)
        {
            //transform by itself is just this game object's transform
            //.rotation is this object's Quaternion rotation in WORLDSPACE, not the localrotation
            // Unity doesn't store rotation information as angles, it stores it as Quaternions. Quaternion.Euler is needed to create a Quaternion value with the angles we want
            // I just set the y rotation angle to what I needed for it to face the camera, depending on the local x scale determined earlier
            transform.rotation = Quaternion.Euler(0f, 45f, 0f);
        } else
        {
            transform.rotation = Quaternion.Euler(0f, -135f, 0f);
        }        
    }
}
