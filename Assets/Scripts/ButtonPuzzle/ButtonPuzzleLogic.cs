using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ButtonPuzzleLogic : MonoBehaviour
{
    public int positionInPuzzleOrder;
    public ButtonPuzzleCounter buttonPuzzleCounter;

    bool buttonIsGreen;
    public bool canUse = true;

    private Renderer thisRenderer;

    public VisualEffect GreenVFX;
    public VisualEffect RedVFX;

    public AudioSource GreenVFXSOUND;
    public AudioSource RedVFXSOUND;

    private void Start()
    {
        thisRenderer = gameObject.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canUse)
        {
            canUse = false;
            buttonIsGreen = buttonPuzzleCounter.TryCounting(positionInPuzzleOrder);

            if (buttonIsGreen)
            {
                Debug.Log("Platform Worked");
                setColorGreen();
            }
            else
            {
                Debug.Log("Platform Did Not Work");
                RedVFXSOUND.Play();
            }
        }
    }

    public void setColorRedThenResetToBlue()
    {
        Debug.Log("set color red" + positionInPuzzleOrder);
        thisRenderer.material.SetFloat("_REDNESS", 1f);
        thisRenderer.material.SetFloat("_GREENNESS", 0f);

        GreenVFX.Stop();
        RedVFX.Play();

        StartCoroutine(ChangeToBlueAfterDelay());
    }

    private void setColorGreen()
    {
        thisRenderer.material.SetFloat("_GREENNESS", 1f);
        GreenVFX.Play();
        GreenVFXSOUND.Play();

        // StartCoroutine(GreenSplash());
    }

    IEnumerator GreenSplash()
    {
        GreenVFX.Play();
        yield return new WaitForSeconds(1.25f);
        GreenVFX.Stop();
    }

    IEnumerator ChangeToBlueAfterDelay()
    {
        yield return new WaitForSeconds(1);

        for (float redness = 1f; redness >= 0; redness -= 0.1f)
        {
            thisRenderer.material.SetFloat("_REDNESS", redness);
            yield return new WaitForSeconds(.1f);
        }
    }
}
