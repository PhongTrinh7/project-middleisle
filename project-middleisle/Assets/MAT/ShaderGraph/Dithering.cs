using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dithering : MonoBehaviour
{
    public float ditherOpacity = 0.75f;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Renderer>().material.SetFloat("_Opacity", ditherOpacity);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Renderer>().material.SetFloat("_Opacity", 1);
    }
}
