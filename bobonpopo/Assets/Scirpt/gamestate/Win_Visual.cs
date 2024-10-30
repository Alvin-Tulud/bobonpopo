using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Visual : MonoBehaviour
{
    [SerializeField]
    private Sprite[] flag_Sprites;
    private SpriteRenderer flag_Renderer;
    private ParticleSystem flag_Particles;
    private Win_Check check;

    private bool canCheck;
    private bool hasWon;
    // Start is called before the first frame update
    void Start()
    {
        flag_Renderer = GetComponent<SpriteRenderer>();
        flag_Particles = GetComponent<ParticleSystem>();
        check = GetComponent<Win_Check>();


        canCheck = true;
        hasWon = false;
        flag_Renderer.sprite = flag_Sprites[0];
        flag_Particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

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

        Debug.Log("checking");

        hasWon = check.getHasWon();

        if (hasWon)
        {
            flag_Renderer.sprite = flag_Sprites[1];
            flag_Particles.Play();

            Debug.Log("win");
        }
        else
        {
            flag_Renderer.sprite = flag_Sprites[0];
            flag_Particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            Debug.Log("not win");
        }

        canCheck = true;
    }
}
