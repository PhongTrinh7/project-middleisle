using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
    public GameObject inspector;
    public Image icon;
    Item item;
    Item oldItem;
    public Text description;

    public void AddItem(Item newItem)
    {
        oldItem = item;
        item = newItem;
        if (inspector.activeSelf == false)
        {
            oldItem = newItem;
        }
        icon.sprite = item.icon;
        icon.enabled = true;
        description.text = item.description;
    }

    public void OpenInspector()
    {
        if (oldItem == item)
        {
            inspector.SetActive(!inspector.activeSelf);
            Debug.Log("Toggling Inspector");
        }
    }
}
