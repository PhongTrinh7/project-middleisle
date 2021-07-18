using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;
    public PlayerMove character;
    public GameManage gamemanage;

    public virtual void Interact()
    {
        gamemanage.Interacting();

        //I am assuming the default interaction with objects is a dialogue pop up.
        //If that's not the case, place this method in an override on a child class.
        // gamemanage.StartDialogue();

        // - I think pickups will actually be the default interaction (or perhaps just as many). I added the override in the child class "curio"  - Jack
    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
            else
            {
                gamemanage.TooFar();
                hasInteracted = true;
                character.RemoveFocus();
            }
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
        hasInteracted = false;
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
