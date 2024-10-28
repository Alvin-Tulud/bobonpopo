using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool moveObj(Vector2 direction)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(transform.position, 0.1f, direction, 1f);

        return false;
    }
}
