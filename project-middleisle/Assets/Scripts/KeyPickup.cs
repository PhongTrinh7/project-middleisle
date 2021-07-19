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
        if (item.IsKey = true)
        {
            door.unlocked = true;
        }
        Debug.Log("Picked Up " + item.name);
        gamemanage.pickup(item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}