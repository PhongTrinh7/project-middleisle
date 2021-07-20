using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : Interactable
{
    public Animator animator;
    public bool unlocked = false;
    public bool locked = false;

    public override void Interact()
    {
        if (unlocked == true)
        {
            animator.SetBool("doorOpen", true);
            if (animator.GetBool("doorClose") == true)
            {
                GameManage.gamemanager.Locked();
                AudioManager.Audio.Play("Doorlocked");
            }
        }
        else
        {
            GameManage.gamemanager.Locked();
            AudioManager.Audio.Play("Doorlocked");
        }
        if (unlocked == true && locked == false)
            AudioManager.Audio.Play("Dooropen");
    }

    public void CloseDoor()
    {
        animator.SetBool("doorClose", true);
        animator.SetBool("doorOpen", false);
        if (locked == false)
        {
            AudioManager.Audio.Play("Doorclose");
            locked = true;
        }
    }
}
