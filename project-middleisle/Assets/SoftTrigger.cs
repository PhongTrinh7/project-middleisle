using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>() != null)
        {
            other.GetComponent<PlayerMove>().SoftHard = false;
            Debug.Log("SoftTerrain");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMove>() != null)
        {
            other.GetComponent<PlayerMove>().SoftHard = true;
            Debug.Log("HardTerrain");
        }
    }
}
