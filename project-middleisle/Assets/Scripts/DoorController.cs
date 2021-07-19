using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : Interactable
{
    public Animator animator;
    public bool unlocked = false;

    public override void Interact()
    {
        if (unlocked == true)
        {
            animator.SetBool("doorOpen", true);
            if (animator.GetBool("doorClose") == true)
            {
                gamemanage.Locked();
            }
        }
        else
        {
            gamemanage.Locked();
        }
    }

    public void CloseDoor()
    {
        animator.SetBool("doorClose", true);
        animator.SetBool("doorOpen", false);
    }
}
