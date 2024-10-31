using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Scene_Switch_Visual : MonoBehaviour
{
    [SerializeField]
    private static AnimationClip[] sceneTransitions;
    private static Animation transitionAnimation;

    // Start is called before the first frame update
    private void Awake()
    {
        transitionAnimation = GetComponent<Animation>();
        transitionAnimation.clip = transitionAnimation.GetClip("Level_Start");
        transitionAnimation.Play();
    }

    public static void playEndTransition()
    {
        transitionAnimation.clip = transitionAnimation.GetClip("Level_End");
        transitionAnimation.Play();
    }
}
