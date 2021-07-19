using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public string description = "Empty Description";
    public bool IsKey = false;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}