using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialOutside : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>() != null)
        {
            AudioManager.Audio.StopPlay("InteriorAmbiance");
            AudioManager.Audio.Play("ExteriorAmbiance");
            AudioManager.Audio.Ambiance = false;
            Debug.Log("PLAYExterior");
            gameObject.SetActive(false);
        }
    }

}
