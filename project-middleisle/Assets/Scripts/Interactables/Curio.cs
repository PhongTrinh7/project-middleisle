using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curio : Interactable
{
    public Dialogue[] dialogues; // Use the inspector to place the dialogue in here with each textbox being a single element of the array.
    public Queue<Dialogue> dialogue;

    private void Start()
    {
        dialogue = new Queue<Dialogue>();
    }

    public override void Interact()
    {
        // This is in case curios can be interacted with more than once.
        foreach (Dialogue d in dialogues)
        {
            dialogue.Enqueue(d);
        }

        StartCoroutine(DialogueManager.Instance.StartDialogue(dialogue));
    }
}

    