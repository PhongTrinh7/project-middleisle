using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : Interactable
{
    public Animator animator;
    public bool locked = true;

    public override void Interact()
    {
        if (!locked)
        {
            animator.SetBool("doorOpen", true);
            AudioManager.Audio.Play("Dooropen");
        }
        else
        {
            GameManage.gamemanager.Locked();
            AudioManager.Audio.Play("Doorlocked");
        }
        PlayerMove.character.animator.SetTrigger("Pickup");
    }

    public void CloseDoor()
    {
        animator.SetBool("doorClose", true);
        animator.SetBool("doorOpen", false);
        AudioManager.Audio.Play("Doorclose");
        locked = true;
    }
}
