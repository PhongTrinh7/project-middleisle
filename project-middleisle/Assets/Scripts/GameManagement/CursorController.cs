using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public static CursorController cursor;
    public Texture2D pointer, selector;
    public Camera mainCamera;
    public Vector2 mouseVector;

    void Awake()
    {
        cursor = this;
        Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        mouseVector = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mouseVector);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                Cursor.SetCursor(selector, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
            }
        }
    }

    public void ActivateSelector()
    {
        Cursor.SetCursor(selector, Vector2.zero, CursorMode.Auto);
        Debug.Log("Activating Selector");
    }

    public void ActivatePointer()
    {
        Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
    }
}
