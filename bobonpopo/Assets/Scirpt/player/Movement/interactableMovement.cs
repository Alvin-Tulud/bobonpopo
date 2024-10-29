using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask obstacleCheck;
    private int OriginalLayer;
    private int TempLayer;

    private Grid worldGrid;

    private bool onTheMove = false;


    private float moveTimer = 0;
    private const float moveMaxTimer = 10;
    private Vector2 direction;
    private Vector3 interInitPos;
    private Vector3 interEndPos;
    // Start is called before the first frame update
    void Start()
    {
        OriginalLayer = gameObject.layer;
        TempLayer = 2;//ignore raycast layer

        worldGrid = FindAnyObjectByType<Grid>();
    }

    private void FixedUpdate()
    {
        if (onTheMove)
        {
            if (moveTimer < moveMaxTimer)
            {
                transform.position = Vector3.Lerp(interInitPos, interEndPos, moveTimer / moveMaxTimer);

                moveTimer++;
            }
            else
            {
                transform.position = worldGrid.LocalToCell(interEndPos);

                moveTimer = 0;

                interInitPos = Vector3.zero;
                interEndPos = Vector3.zero;

                onTheMove = false;
            }
        }
    }

    //return true if player and object can move false if it cant
    public bool moveObj(Vector2 direction)
    {
        gameObject.layer = TempLayer;

        RaycastHit2D hit;
        hit = Physics2D.CircleCast(transform.position, 0.1f, direction, 1f, obstacleCheck);

        gameObject.layer = OriginalLayer;


        
        
        /*
         * First check if any obstacle is in the way, then check if the object is a key or not
         * If current object is a key, prevent any movement IF the obstacle is not a door
         * If not, just prevent any kind of movement
         */
        if (hit) { 

            if (transform.CompareTag("Key"))
            {
                if (!hit.transform.CompareTag("Door"))
                {
                    return false;
                }
            } 
            else
            {
                return false;
            }   
        }



        this.direction = direction;
        interInitPos = worldGrid.LocalToCell(transform.position);
        interEndPos = worldGrid.LocalToCell(transform.position + (Vector3)direction);

        onTheMove = true;

        return true;
    }
}
