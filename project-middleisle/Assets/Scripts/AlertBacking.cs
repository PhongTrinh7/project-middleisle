using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertBacking : MonoBehaviour
{
    public RectTransform background;
    public Text alertText;
    public float padding;

    public void Resize()
    {
        background.sizeDelta = new Vector2(alertText.preferredWidth + padding, alertText.fontSize + 10);
    }
}
