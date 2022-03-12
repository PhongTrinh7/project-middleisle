using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dithering : MonoBehaviour
{
    public float ditherOpacity = 0.75f;
    public float opacityAdjust = 5f;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Renderer>() != null)
        {
            other.GetComponent<Renderer>().material.SetFloat("_Opacity", ditherOpacity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Renderer>() != null)
        {
            other.GetComponent<Renderer>().material.SetFloat("_Opacity", 1f);
        }
    }
}

