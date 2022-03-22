using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzleLogic : MonoBehaviour
{
    public int positionInPuzzleOrder;
    public ButtonPuzzleCounter buttonPuzzleCounter;

    bool buttonIsWorking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !buttonIsWorking)
        {
            buttonIsWorking = buttonPuzzleCounter.TryCounting(positionInPuzzleOrder);

            if (buttonIsWorking)
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
    }

    private void setColorGreen()
    {

    }
}
