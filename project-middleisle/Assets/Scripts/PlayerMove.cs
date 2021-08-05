using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour, IsoMove.IPlayerActions
{
    public Animator animator;
    public CharacterController controller;
    public Vector3 _direction;
    public float _speed;
    public float _sprintSpeed;
    public Vector2 mouseVector;
    public Camera mainCamera;
    public Interactable focus;
    public InventoryUI inventory;
    public GameObject dialoguePopUp;
    public GameObject ingameUI;

    public static PlayerMove character;

    void Start()
    {
        character = this;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    #region Movement

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
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

        mouseVector = Mouse.current.position.ReadValue();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

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

    #endregion

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (dialoguePopUp.activeSelf == false && ingameUI.activeSelf == false)
        {
            if (context.performed)
            {
                inventory.OpenInventory();
                AudioManager.Audio.Play("OpenInventory");
            }
        }
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DialogueManager.Instance.dialogueskip = true;
            if (DialogueManager.Instance.advanceDialoguekey == true)
                DialogueManager.Instance.AdvanceDialogue();
        }
    }

    public void OnSkipFinish(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DialogueManager.Instance.dialogueskip = false;
        }

    }

    #region Interaction

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

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
            else
            {
                RemoveFocus();
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

    #endregion

    private void Step()
    {
        AudioManager.Audio.Walking();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        SaveSystem.SaveInventory();
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        //Inventory load.
        Inventory.instance.items.Clear();

        foreach (string i in SaveSystem.LoadInventory().itemNames)
        {
            Debug.Log(i);
            Inventory.instance.Add((Item)Instantiate(Resources.Load("Items/" + i))); // Make sure to name your scriptable objects accordingly. Found in Resources/Items directory.
        }
    }

    public void OnInGameMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Esc");
        if (context.performed)
        {
            GameManage.gamemanager.OpenMenu();
        }
    }
}