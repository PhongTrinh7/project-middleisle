using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertBacking : MonoBehaviour
{
    public Canvas canvas;
    public Transform player;
    public RectTransform background;
    public Text alertText;
    public float padding;
    public float alertDistance = 200f;

    public void Resize()
    {
        Vector2 v = (Vector2)Camera.main.WorldToScreenPoint(player.position) + new Vector2(0, alertDistance) * canvas.scaleFactor;
        background.position = v;
        background.sizeDelta = new Vector2(alertText.preferredWidth + padding, alertText.fontSize + 10);
    }

    private void Update()
    {
        Vector2 v = (Vector2)Camera.main.WorldToScreenPoint(player.position) + new Vector2(0, alertDistance) * canvas.scaleFactor;
        background.position = v;
    }
}
