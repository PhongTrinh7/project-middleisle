using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController cursor;
    public Texture2D pointer, selector;
    
    void Awake()
    {
        cursor = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSelector()
    {
        Cursor.SetCursor(selector, Vector2.zero, CursorMode.Auto);
    }
}
