using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private Grid worldGrid;

    private bool canInput = true;

    private bool canMove = false;
    private float moveTimer = 0;
    private const float moveMaxTimer = 25;
    private Vector2 direction;
    private Vector3 playerInitPos;
    private Vector3 playerEndPos;
    private GameObject ObjtoMove;
    private Vector3 objInitPos;
    private Vector3 objEndPos;

    // Start is called before the first frame update
    void Start()
    {
        worldGrid = FindAnyObjectByType<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            //input up
            if (Input.GetKeyDown(KeyCode.W))
            {
                directionChecker(Vector2.up);
            }

            //input down
            else if (Input.GetKeyDown(KeyCode.S))
            {
                directionChecker(Vector2.down);
            }

            //input right
            else if (Input.GetKeyDown(KeyCode.D))
            {
                directionChecker(Vector2.right);
            }

            //input left
            else if (Input.GetKeyDown(KeyCode.A))
            {
                directionChecker(Vector2.left);
            }
        }



        if (canMove)
        {
             canInput = false;

            if (moveTimer < moveMaxTimer)
            {
                transform.position = Vector3.Lerp(playerInitPos, playerEndPos, moveTimer / moveMaxTimer);

                if (ObjtoMove != null)
                {
                    ObjtoMove.transform.position = Vector3.Lerp(objInitPos, objEndPos, moveTimer / moveMaxTimer);
                }

                moveTimer++;
            }
            else
            {
                transform.position = playerEndPos;

                if (ObjtoMove != null)
                {
                    ObjtoMove.transform.position = objEndPos;
                }

                moveTimer = 0;

                playerInitPos = Vector3.zero;
                playerEndPos = Vector3.zero;
                objInitPos = Vector3.zero;
                objEndPos = Vector3.zero;

                ObjtoMove = null;
            }
        }
    }

    private void directionChecker(Vector2 direction)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(transform.position, 0.1f, direction, 1f);
        
        //check if interactable object is infront of player
            //check again if a wall is infront of that
                //if nothing move both player and object
        if (hit.transform.gameObject.layer == 7)
        {
            ObjtoMove = hit.transform.gameObject;

            hit = Physics2D.CircleCast(transform.position, 0.1f, direction, 2f, 8);

            if (hit.transform.gameObject.layer != 8 && hit.transform.gameObject.layer != 7)
            {
                this.direction = direction;
                canMove = true;
                playerInitPos = transform.position;
                playerEndPos = transform.position + (Vector3) direction;
                objInitPos = hit.transform.position;
                objEndPos = hit.transform.position + (Vector3)direction;
            }
        }

        //check if nothing is infront of player
            //move player
        else if (hit.transform.gameObject.layer != 8)
        {
            this.direction = direction;
            canMove = true;
            playerInitPos = transform.position;
            playerEndPos = transform.position + (Vector3)direction;
        }
    }
}
