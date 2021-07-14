using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public GameObject player;
    private Rigidbody playerRigid;


    public float movementSpeed;
    public float speedMultiplier;

    public float jumpForce;

    private bool jump;



    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

    }


    void Movement()
    {

        if (jump)
        {
            playerRigid.velocity = new Vector3(movementSpeed * speedMultiplier * Input.GetAxisRaw("Horizontal"), jumpForce, movementSpeed * speedMultiplier * Input.GetAxisRaw("Vertical"));
        }
        else
        {
            playerRigid.velocity = new Vector3(movementSpeed * speedMultiplier * Input.GetAxisRaw("Horizontal"), playerRigid.velocity.y, movementSpeed * speedMultiplier * Input.GetAxisRaw("Vertical"));
        }









    }
}