using UnityEngine;

public class KeyPickup : Interactable
{
    public Item item;
    public DoorController door;

    public override void Interact()
    {
        KeyPickUp();
    }

    void KeyPickUp()
    {
        AudioManager.Audio.Play("Pickup");
        if (item.IsKey = true)
        {
            door.unlocked = true;
        }
        Debug.Log("Picked Up " + item.name);
        GameManage.gamemanager.pickup(item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}