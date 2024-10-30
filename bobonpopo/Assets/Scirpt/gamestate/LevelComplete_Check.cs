using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete_Check : MonoBehaviour
{
    private Win_Check[] win_Checks;
    private GridMovement[] gridMovements;
    private AudioSource levelComplete_audio;

    private bool canCheck;
    private bool levelComplete;


    // Start is called before the first frame update
    void Start()
    {
        win_Checks = FindObjectsByType<Win_Check>(FindObjectsSortMode.None);
        gridMovements = FindObjectsByType<GridMovement>(FindObjectsSortMode.None);
        levelComplete_audio = GetComponent<AudioSource>();

        canCheck = true;
        levelComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheck)
        {
            StartCoroutine(checkLevelComplete());
        }
    }

    IEnumerator checkLevelComplete()
    {
        canCheck = false;
        yield return new WaitForSeconds(0.25f);

        foreach (Win_Check check in win_Checks)
        {
            if (!check.getHasWon() || !check.getOnFlag())
            {
                levelComplete = false;
                break;
            }
            else
            {
                levelComplete = true;
            }
        }

        if (levelComplete)
        {
            GameObject.Find("BGM").GetComponent<AudioSource>().Stop();

            levelComplete_audio.Play();

            foreach(GridMovement movement in gridMovements)
            {
                movement.setMove(false);
            }


        }

        canCheck = true;
    }
}
