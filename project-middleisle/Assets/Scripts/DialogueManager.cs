using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; //Makes it accessible from anywhere. So we don't need a reference to this object for every curio in the scene.
    public GameObject dialoguePopUp;
    public Text dialogueText;
    private bool inDialogue; //Potentially for future use.
    private bool textScrolling;
    public Button advanceDialogueButton;
    private Queue<string> dialogue;
    public float dialogueDelay = 0.5f;
    public float textScrollDelay = 0.1f;
    public bool dialogueskip = false;
    public bool advanceDialoguekey = false;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator StartDialogue(Queue<string> dialogue)
    {
        inDialogue = true;
        dialoguePopUp.SetActive(true);
        this.dialogue = dialogue;
        advanceDialogueButton.interactable = false;
        advanceDialoguekey = false;
        PlayerMove.character._direction = Vector3.zero;
        

        // This is to delay the text scrolling before pop up animation finishes.
        yield return new WaitForSecondsRealtime(dialogueDelay);

        StartCoroutine(ScrollText(dialogue.Dequeue()));
    }

    public void AdvanceDialogue()
    {
        if (dialogue.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(ScrollText(dialogue.Dequeue()));
    }

    IEnumerator ScrollText(string line)
    {
        advanceDialogueButton.interactable = false;
        advanceDialoguekey = false;
        dialogueText.text = "";

        foreach(char c in line)
        {
            dialogueText.text += c;
            AudioManager.Audio.Play("Lissevoicefast");
            // Use this if you'd like to modify the text scroll rate, otherwise it will scroll 1 character per frame.
            if (dialogueskip == false)
            {
                yield return new WaitForSecondsRealtime(0.04f);
            }
            else
            {
                yield return null;
            }
        }

        advanceDialogueButton.interactable = true;
        advanceDialoguekey = true;
    }

    public void EndDialogue()
    {
        dialogue.Clear();
        dialogueText.text = "";
        inDialogue = false;
        dialoguePopUp.SetActive(false);
        

    }
}
