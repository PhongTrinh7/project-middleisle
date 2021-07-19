using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    //UI
    public Text notification;
    public AlertBacking alertBacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interacting()
    {
        StopAllCoroutines();
        StartCoroutine(sendNotification("INTERACTING", 2));
    }

    public void TooFar()
    {
        StopAllCoroutines();
        StartCoroutine(sendNotification("\"I need to get closer.\"", 2));
    }

    public void Locked()
    {
        StopAllCoroutines();
        StartCoroutine(sendNotification("\"It won't open.\"", 2));
    }

    public void pickup(string itemName)
    {
        StopAllCoroutines();
        StartCoroutine(sendNotification("Picked up: " + itemName, 2));
    }

    IEnumerator sendNotification(string text, int time)
    {
        alertBacking.gameObject.SetActive(true);
        notification.text = text;
        alertBacking.Resize();
        yield return new WaitForSeconds(time);
        notification.text = "";
        alertBacking.gameObject.SetActive(false);

    }
}
