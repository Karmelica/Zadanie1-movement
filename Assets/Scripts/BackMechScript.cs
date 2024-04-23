using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackMechScript : MonoBehaviour
{
    public Image XD;
    private Vector2 mousePos;

    private void Update()
    {
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos.x *= Screen.width;
        mousePos.y *= Screen.height;

        if (Input.GetMouseButton(1))
        {
            XD.rectTransform.position = mousePos;
        }
    }
}
