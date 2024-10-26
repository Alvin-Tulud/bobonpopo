using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Has_Box : MonoBehaviour
{
    private bool hasbox = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Box"))
        {
            hasbox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasbox = false;
    }

    public bool getHasBox() { return hasbox; }
}
