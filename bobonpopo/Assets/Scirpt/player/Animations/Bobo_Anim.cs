using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobo_Anim : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool move = GameObject.Find("Bobo_Physics").GetComponent<GridMovement>().getMove();

        if (move)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
