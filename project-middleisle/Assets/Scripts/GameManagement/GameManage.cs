using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    //UI
    public Text notification;
    public GameObject dialoguePopUp;
    private bool inDialogue; //Potentially for future use.

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
        StartCoroutine(sendNotification("INTERACTING", 2));
    }

    public void TooFar()
    {
        StartCoroutine(sendNotification("MOVE CLOSER", 2));
    }

    public void StartDialogue()
    {
        inDialogue = true;
        dialoguePopUp.SetActive(true);
    }

    public void AdvanceDialogue()
    {
        //TODO.
        Debug.Log("Advance Dialogue");
    }

    public void EndDialogue()
    {
        inDialogue = false;
        dialoguePopUp.SetActive(false);
    }

    public void pickup()
    {
        StartCoroutine(sendNotification("CHECK INVENTORY", 2));
    }

    IEnumerator sendNotification(string text, int time)
    {
        notification.text = text;
        yield return new WaitForSeconds(time);
        notification.text = "";

    }
}
