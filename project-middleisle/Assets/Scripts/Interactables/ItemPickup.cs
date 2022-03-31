using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public DoorController door;
    public bool IsHybrid;
    public Curio TargetCurio;

    public override void Interact()
    {
        ItemPickUp();
    }

    void ItemPickUp ()
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

            if(IsHybrid == true)
            {
                TargetCurio.Interact();
            }
        }
    }
}
