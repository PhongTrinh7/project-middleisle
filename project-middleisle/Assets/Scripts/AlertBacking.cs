using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertBacking : MonoBehaviour
{
    public Transform player;
    public RectTransform background;
    public Text alertText;
    public float padding;

    public void Resize()
    {
        background.sizeDelta = new Vector2(alertText.preferredWidth + padding, alertText.fontSize + 10);
    }

    private void Update()
    {
        Vector2 v = (Vector2)Camera.main.WorldToScreenPoint(player.position) + new Vector2(0, 150);
        background.position = v;
    }
}
