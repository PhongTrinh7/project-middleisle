using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideTrigger : MonoBehaviour
{
    public InitialOutside initializer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>() != null)
        {
            initializer.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMove>() != null)
        {
            if(AudioManager.Audio.Ambiance == false)
            {
                AudioManager.Audio.Ambiance = true;
                AudioManager.Audio.StopPlay("ExteriorAmbiance");
                AudioManager.Audio.Play("InteriorAmbiance");
                Debug.Log("PlayInterior");
            }
            else
            {
                AudioManager.Audio.Ambiance = false;
                AudioManager.Audio.StopPlay("InteriorAmbiance");
                AudioManager.Audio.Play("ExteriorAmbiance");
                Debug.Log("PlayExterior");
            }
        }
    }
}
