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
        GameManage.gamemanager.pickup(item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
