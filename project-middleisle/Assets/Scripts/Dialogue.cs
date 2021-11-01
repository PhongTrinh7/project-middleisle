using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string speakerName;
    public string words;
    public Sprite leftSpeaker;
    public Sprite rightSpeaker;
    public bool left;
    public AudioClip voice;
    public bool intermission;
}
