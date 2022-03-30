using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public float flickerFrequency = 0.03f;
    public GameObject FlickerSound;

    void Update()
    {
        if(isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        FlickerSound.SetActive(!FlickerSound.activeSelf);
        timeDelay = Random.Range(0.01f, flickerFrequency);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        FlickerSound.SetActive(!FlickerSound.activeSelf);
        timeDelay = Random.Range(0.01f, flickerFrequency);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
