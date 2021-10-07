using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curio : Interactable
{
    public Sprite characterPortrait;
    public string[] strings; //Use the inspector to place the dialogue in here with each textbox being a single element of the array.
    public Queue<string> dialogue;

    private void Start()
    {
        dialogue = new Queue<string>();
    }

    public override void Interact()
    {
        if (characterPortrait != null)
        {
            DialogueManager.Instance.SetRightSidePortrait(characterPortrait);
        }

        // This is in case curios can be interacted with more than once.
        foreach (string s in strings)
        {
            dialogue.Enqueue(s);
        }

        StartCoroutine(DialogueManager.Instance.StartDialogue(dialogue));
    }
}

    