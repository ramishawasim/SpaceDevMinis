using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzleCounter : MonoBehaviour
{
    private int GlobalCounter;
    public int NumberOfPlatforms;
    public GameObject RewardEgg;

    public List<ButtonPuzzleLogic> ButtonPuzzleLogics;

    public bool TryCounting(int positionInPuzzleOrder)
    {
        if (positionInPuzzleOrder == GlobalCounter)
        {
            GlobalCounter++;
            if (GlobalCounter == NumberOfPlatforms)
            {
                PuzzleComplete();
            }
            return true;
        } else
        {
            GlobalCounter = 0;
            for (int i = 0; i < NumberOfPlatforms; i++)
            {
                ButtonPuzzleLogics[i].setColorRedThenResetToBlue();
                StartCoroutine(enableButtonsAfterDelay());
            }
            return false;
        }
    }

    private void PuzzleComplete()
    {
        Debug.Log("Puzzle Complete");
        RewardEgg.SetActive(true);
        StartCoroutine(TurnOffGreenVFX());
    }

    IEnumerator enableButtonsAfterDelay()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < NumberOfPlatforms; i++)
        {
            ButtonPuzzleLogics[i].canUse = true;
        }
    }

    IEnumerator TurnOffGreenVFX()
    {
        yield return new WaitForSeconds(1.25f);
        for (int i = 0; i < NumberOfPlatforms; i++)
        {
            ButtonPuzzleLogics[i].GreenVFX.Stop();
        }
    }

}
