using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; //Makes it accessible from anywhere. So we don't need a reference to this object for every curio in the scene.

    //Canvas Object References
    public GameObject dialoguePopUp;
    public Button advanceDialogueButton;

    //Text
    public Text dialogueText;
    public Text nameTagText;
    private Queue<Dialogue> dialogues;
    private bool inDialogue; //Potentially for future use.

    //Scrolling
    private bool textScrolling;
    public float dialogueDelay = 0.5f;
    public float textScrollDelay = 0.1f;
    public bool dialogueskip = false;
    public bool advanceDialoguekey = false;

    //Portraits
    public Image rightPortrait;
    public Image leftPortrait;
    public Color32 speakingColor;
    public Color32 fadeColor;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator StartDialogue(Queue<Dialogue> dialogues)
    {
        this.dialogues = dialogues;
        Dialogue currentDialogue = this.dialogues.Dequeue();

        SetDialogueScene(currentDialogue);

        inDialogue = true;
        dialoguePopUp.SetActive(true);
        advanceDialogueButton.interactable = false;
        advanceDialoguekey = false;
        PlayerMove.character._direction = Vector3.zero;
        

        // This is to delay the text scrolling before pop up animation finishes.
        yield return new WaitForSecondsRealtime(dialogueDelay);

        dialogueText.gameObject.SetActive(true);
        StartCoroutine(ScrollText(currentDialogue.words));
    }

    public void AdvanceDialogue()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();

        Dialogue currentDialogue = dialogues.Dequeue();
        SetDialogueScene(currentDialogue);
        StartCoroutine(ScrollText(currentDialogue.words));
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
        dialogues.Clear();
        dialogueText.text = "";
        inDialogue = false;
        dialoguePopUp.SetActive(false);

        leftPortrait.gameObject.SetActive(false);
        rightPortrait.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    private void SetDialogueScene(Dialogue dialogue)
    {
        if (dialogue.leftSpeaker != null)
        {
            SetLeftPortrait(dialogue.leftSpeaker);
        }

        if (dialogue.rightSpeaker != null)
        {
            SetRightPortrait(dialogue.rightSpeaker);
        }

        if (dialogue.left)
        {
            LeftSpeaking(dialogue.words, dialogue.speakerName);
        }
        else
        {
            RightSpeaking(dialogue.words, dialogue.speakerName);
        }
    }

    public void SetRightPortrait(Sprite sprite)
    {
        rightPortrait.GetComponent<Animator>().enabled = true;
        rightPortrait.gameObject.SetActive(true);
        rightPortrait.sprite = sprite;
    }

    public void SetLeftPortrait(Sprite sprite)
    {
        leftPortrait.GetComponent<Animator>().enabled = true;
        leftPortrait.gameObject.SetActive(true);
        leftPortrait.sprite = sprite;
    }

    public void LeftSpeaking(string text, string name)
    {
        nameTagText.text = name;
        leftPortrait.color = speakingColor;
        rightPortrait.color = fadeColor;
        dialogueText.text = text;
    }

    public void RightSpeaking(string text, string name)
    {
        nameTagText.text = name;
        rightPortrait.color = speakingColor;
        leftPortrait.color = fadeColor;
        dialogueText.text = text;
    }
}
