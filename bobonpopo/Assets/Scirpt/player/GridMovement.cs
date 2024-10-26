using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private Grid worldGrid;

    private bool onTheMove = false;


    private float moveTimer = 0;
    private const float moveMaxTimer = 10;
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
        //take in user input when enabled
        //usual wasd movement
        //calls function that takes in the direction
        if (!onTheMove)
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
    }

    private void FixedUpdate()
    {
        //move user when enabled
        //lerp between grid cells within half a second
        //move player (and object if given)
        //resets variables and re enables input
        if (onTheMove)
        {
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
                transform.position = worldGrid.LocalToCell(playerEndPos);

                if (ObjtoMove != null)
                {
                    ObjtoMove.transform.position = worldGrid.LocalToCell(objEndPos);
                }

                moveTimer = 0;

                playerInitPos = Vector3.zero;
                playerEndPos = Vector3.zero;
                objInitPos = Vector3.zero;
                objEndPos = Vector3.zero;

                ObjtoMove = null;

                onTheMove = false;
            }
        }

    }


    //throws a raycast in the direction of player movement
    //if it hits an interactable object store the object
        //store player pos
        //store obj pos
        //enable move disable input
    //else if not wall or interactable 
        //store player pos
        //enable move disable input
    //else not a valid place to move so do nothing
    private void directionChecker(Vector2 direction)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(transform.position, 0.1f, direction, 1f);

        if (!hit)
        {
            Debug.Log("Nothing");
        }

        // Do nothing if player will hit a wall
        if (hit.transform.gameObject.layer == 8)
        {
            return;
        }

        // See if an interactable can be moved if player hits one
        if (hit.transform.gameObject.layer == 7)
        {
            ObjtoMove = hit.transform.gameObject;
            hit = Physics2D.CircleCast(transform.position, 0.1f, direction, 2f, 8);

            // If object is immovable so is the player
            if (hit.transform.gameObject.layer == 8 || hit.transform.gameObject.layer == 7)
            {
                Debug.Log("Immovable box");
                return;
            }
            Debug.Log("Should move box");
            objInitPos = worldGrid.LocalToCell(hit.transform.position);
            objEndPos = worldGrid.LocalToCell(hit.transform.position + (Vector3)direction);
        }

        this.direction = direction;
        playerInitPos = worldGrid.LocalToCell(transform.position);
        playerEndPos = worldGrid.LocalToCell(transform.position + (Vector3)direction);

        onTheMove = true;
    }
}
