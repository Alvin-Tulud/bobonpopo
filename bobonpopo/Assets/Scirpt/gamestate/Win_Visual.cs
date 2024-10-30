using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Visual : MonoBehaviour
{
    [SerializeField]
    private Sprite[] flag_Sprites;
    private SpriteRenderer flag_Renderer;
    private ParticleSystem flag_Particles;
    [SerializeField]
    private AudioClip[] flag_sounds;
    private AudioSource flag_AudioSource;
    private Win_Check check;

    private bool canCheck;
    private bool hasWon;
    private bool previousState;
    // Start is called before the first frame update
    void Start()
    {
        flag_Renderer = GetComponent<SpriteRenderer>();
        flag_Particles = GetComponent<ParticleSystem>();
        flag_AudioSource = GetComponent<AudioSource>();
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



        hasWon = check.getHasWon();

        if (hasWon)
        {
            flag_Renderer.sprite = flag_Sprites[1];
            flag_Particles.Play();

            playAudio(1);
        }
        else
        {
            flag_Renderer.sprite = flag_Sprites[0];
            flag_Particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            playAudio(0);
;        }

        previousState = hasWon;

        canCheck = true;
    }

    private void playAudio(int index)
    {
        if (previousState != hasWon)
        {
            flag_AudioSource.clip = flag_sounds[index];

            float pitch = Random.Range(0.7f, 1.3f);

            flag_AudioSource.pitch = pitch;

            flag_AudioSource.Play();
        }
    }
}
