using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    bool isFocus = false;
    Transform player;

    public float InteractCoolDown = 0f;
    public float CoolDownTime = 1f;

    public virtual void Interact()
    {
        GameManage.gamemanager.Interacting();

        //I am assuming the default interaction with objects is a dialogue pop up.
        //If that's not the case, place this method in an override on a child class.
        // gamemanage.StartDialogue();

        // - I think pickups will actually be the default interaction (or perhaps just as many). I added the override in the child class "curio"  - Jack
    }

    void Update()
    {
        if(InteractCoolDown >= 0f)
        {
            InteractCoolDown -= Time.deltaTime;
        }

        if (isFocus && InteractCoolDown <= 0f)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                InteractCoolDown = CoolDownTime;
            }
            else
            {
                GameManage.gamemanager.TooFar();
                InteractCoolDown = CoolDownTime;
                PlayerMove.character.RemoveFocus();
            }
            isFocus = false;
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        InteractCoolDown = 0f;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
