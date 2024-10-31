using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Management : MonoBehaviour
{
    public void nextLevel(int buildindex)
    {
        StartCoroutine(exitScene(buildindex));

    }

    IEnumerator exitScene(int index)
    {
        Scene_Switch_Visual.playEndTransition();
        yield return new WaitForSeconds(1f);


        SceneManager.LoadScene(index + 1);
    }
}
