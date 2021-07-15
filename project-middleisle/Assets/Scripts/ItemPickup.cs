using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    
    public override void Interact()
    {
        ItemPickUp();
    }

    void ItemPickUp ()
    {
        Debug.Log("Picked Up " + item.name);
        gamemanage.pickup();
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
