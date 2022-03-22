using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzleLogic : MonoBehaviour
{
    public int positionInPuzzleOrder;
    public ButtonPuzzleCounter buttonPuzzleCounter;

    bool buttonIsGreen;
    private bool canUse = true;

    private Renderer thisRenderer;

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
            }
        }
    }

    public void setColorRedThenResetToBlue()
    {
        Debug.Log("set color red" + positionInPuzzleOrder);
        thisRenderer.material.SetFloat("_REDNESS", 1f);
        thisRenderer.material.SetFloat("_GREENNESS", 0f);
        canUse = true;

        StartCoroutine(ChangeToBlueAfterDelay());
    }

    private void setColorGreen()
    {
        thisRenderer.material.SetFloat("_GREENNESS", 1f);
    }

    IEnumerator ChangeToBlueAfterDelay()
    {
        yield return new WaitForSeconds(1);
        thisRenderer.material.SetFloat("_REDNESS", 0f);
    }
}
