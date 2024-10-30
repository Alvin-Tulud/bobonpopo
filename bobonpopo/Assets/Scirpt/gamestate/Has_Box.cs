using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Has_Box : MonoBehaviour
{
    [SerializeField]
    private LayerMask boxMask;
    private int OriginalLayer;
    private int TempLayer;

    private bool canCheck;
    private bool hasbox;

    

    private void Start()
    {
        canCheck = true;
        hasbox = false;

        OriginalLayer = gameObject.layer;
        TempLayer = 2;
    }

    private void Update()
    {
        if (canCheck)
        {
            StartCoroutine(checkBox());
        }
    }

    IEnumerator checkBox()
    {
        canCheck = false;
        gameObject.layer = TempLayer;
        yield return new WaitForSeconds(0.25f);


        RaycastHit2D hit;
        hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, 0f, boxMask);


        if (!hit)
        {
            hasbox = false;
        }
        else
        {
            if (hit.transform.CompareTag("Box"))
            {
                hasbox = true;
            }
        }

        
        canCheck = true;
        gameObject.layer = OriginalLayer;
    }

    public bool getHasBox() { return hasbox; }
}
