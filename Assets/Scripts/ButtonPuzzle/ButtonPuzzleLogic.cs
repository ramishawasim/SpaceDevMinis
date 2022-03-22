using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzleLogic : MonoBehaviour
{
    public int positionInPuzzleOrder;
    public ButtonPuzzleCounter buttonPuzzleCounter;

    bool buttonIsGreen;
    private bool canUse = true;

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
        canUse = true;
    }

    private void setColorGreen()
    {

    }
}
