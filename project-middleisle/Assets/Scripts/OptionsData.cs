using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[System.Serializable]
public class OptionsData
{
    public bool FullScreenPersist;
    public int ResolutionVertical;
    public int ResolutionHorizontal;

    public OptionsData (SettingsMenu settings)
    {
        FullScreenPersist = settings.Fullscreen;
        ResolutionVertical = settings.ResolutionVert;
        ResolutionHorizontal = settings.ResolutionHoriz;
    }
}