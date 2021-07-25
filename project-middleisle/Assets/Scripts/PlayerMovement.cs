using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IsoMove.IPlayerActions
{
    public Animator animator;
    public CharacterController controller;
    public Vector3 _direction;
    public float _speed;
    public float _sprintSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        controller.Move(_direction * _speed * Time.deltaTime);
        if (_direction.magnitude > 0.01)
        {
            animator.SetBool("IsWalking", true);
            transform.rotation = Quaternion.LookRotation(_direction);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
        _direction = IsoVectorConvert(toConvert);
        // Debug.LogError("walking");
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0, 45.0f, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        _speed = _speed + _sprintSpeed;
        animator.SetBool("IsRunning", true);
        // Debug.LogError("running");
    }

    public void OnSprintFinish(InputAction.CallbackContext context)
    {
        _speed = _speed - _sprintSpeed;
        if (context.performed)
        {
            animator.SetBool("IsRunning", false);
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSkipFinish(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
