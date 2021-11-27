using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
    public GameObject inspector;
    public Image icon;
    Item item;
    Item oldItem;
    public Text description1;
    public Text description2;
    public Text description3;
    public Text description4;
    public Text itemName;

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
        description1.text = item.description1;
        description2.text = item.description2;
        description3.text = item.description3;
        description4.text = item.description4;
        itemName.text = item.name;
    }

    public void OpenInspector()
    {
        if (oldItem == item)
        {
            AudioManager.Audio.Play("Click");
            inspector.SetActive(!inspector.activeSelf);
            Debug.Log("Toggling Inspector");
        }
        else
        {
            AudioManager.Audio.Play("Click");
        }
    }
}
