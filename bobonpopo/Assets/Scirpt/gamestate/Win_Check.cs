using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Check : MonoBehaviour
{
    private Has_Box[] Box_Spot_States;
    private bool hasWon;
    private bool canCheck;
    // Start is called before the first frame update
    void Start()
    {
        Box_Spot_States = FindObjectsByType<Has_Box>(FindObjectsSortMode.None);
        hasWon = false;
        canCheck = true;
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

        //Debug.Log("checking");

        foreach(var box in Box_Spot_States)
        {
            if (!box.getHasBox())
            {
                //Debug.Log("no win");
                hasWon = false;
                break;
            }
            else
            {
                //Debug.Log("win");
                hasWon = true;
            }
        }

        canCheck = true;
    }

    public bool getHasWon() { return hasWon; }
}
