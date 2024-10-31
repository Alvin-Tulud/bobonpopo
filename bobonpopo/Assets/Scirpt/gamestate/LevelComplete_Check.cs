using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete_Check : MonoBehaviour
{
    private Win_Check[] win_Checks;
    private GridMovement[] gridMovements;
    private ParticleSystem[] particles;
    private AudioSource levelComplete_audio;
    private Scene_Management sceneSwitcher;

    private bool canCheck;
    private bool levelComplete;
    private bool playOnce;


    // Start is called before the first frame update
    void Start()
    {
        win_Checks = FindObjectsByType<Win_Check>(FindObjectsSortMode.None);
        gridMovements = FindObjectsByType<GridMovement>(FindObjectsSortMode.None);
        particles = FindObjectsByType<ParticleSystem>(FindObjectsSortMode.None);
        levelComplete_audio = GetComponent<AudioSource>();
        sceneSwitcher = FindAnyObjectByType<Scene_Management>();

        canCheck = true;
        levelComplete = false;
        playOnce = false;
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
            //GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
            if (!playOnce)
            {
                levelComplete_audio.Play();
                sceneSwitcher.nextLevel(SceneManager.GetActiveScene().buildIndex);
                playOnce = true;
            }

            foreach(GridMovement movement in gridMovements)
            {
                movement.setMove(false);
            }

            foreach(ParticleSystem particleSystem in particles)
            {
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }

        canCheck = true;
    }
}
