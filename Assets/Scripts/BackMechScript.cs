using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackMechScript : MonoBehaviour
{
    private Vector2 mousePos;
    [SerializeField] private Image core;

    public void OnMouse()
    {
        core = gameObject.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        core = null;
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos.x *= Screen.width;
        mousePos.y *= Screen.height;

        if (Input.GetMouseButton(1) && core != null)
        {
            core.rectTransform.position = mousePos;
        }
    }
}
