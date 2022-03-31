using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridPickup : Interactable
{
    public Item item;
    public DoorController door;

    public override void Interact()
    {
        HybridPickUp();
    }

    void HybridPickUp()
    {
        AudioManager.Audio.Play("Pickup");
        if (item.IsKey == true)
        {
            door.locked = false;
        }
        Debug.Log("Picked Up " + item.name);
        GameManage.gamemanager.pickup(item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            gameObject.SetActive(false);
            GameManage.gamemanager.pickedupObjects.Add(name);
            PlayerMove.character.animator.SetTrigger("Pickup");
        }
    }
}
