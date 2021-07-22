using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    public DoorController door;

    public void OnTriggerEnter(Collider other)
    {
        door.CloseDoor();
        door.InteractCoolDown = 0f;
        Destroy(gameObject);
    }
}
