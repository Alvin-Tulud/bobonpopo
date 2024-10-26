using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Check : MonoBehaviour
{
    private Has_Box[] Box_Spot_States;
    private bool hasWon = false;
    private bool canCheck = true;
    // Start is called before the first frame update
    void Start()
    {
        Box_Spot_States = FindObjectsByType<Has_Box>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheck)
        {
            StartCoroutine(checkWin());
        }
    }

    //every quarter second check all box spots to see if they have a box ontop
        //if they do flip the win variable bool
    IEnumerator checkWin()
    {
        canCheck = false;
        yield return new WaitForSeconds(0.25f);

        foreach(var box in Box_Spot_States)
        {
            if (!box.getHasBox())
            {
                hasWon = false;
                break;
            }
            else
            {
                hasWon = true;
            }
        }

        canCheck = true;
    }
}
