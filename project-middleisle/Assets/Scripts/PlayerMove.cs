using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour, IsoMove.IPlayerActions
{
    public CharacterController controller;
    public Vector3 _direction;
    public float _speed;
    public Vector2 mouseVector;
    public Camera mainCamera;
    public Interactable focus;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(_direction * _speed * Time.deltaTime);
        if (_direction.magnitude > 0.01)
        {
            transform.rotation = Quaternion.LookRotation(_direction);
        }
        mouseVector = Mouse.current.position.ReadValue();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
        _direction = IsoVectorConvert(toConvert);
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0, 45.0f, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DetectObject();
        }
    }

    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(mouseVector);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
            }
        }
        else
        {
            RemoveFocus();
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
    }
}