using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private int selectedOption = 0;
    private bool pressed;

    public List<Button> selectedButton;
    public Button selector;
    public Image filler;
    public Image pointer;

    public void ChangeOption()
    {
        pointer.rectTransform.anchoredPosition = new Vector2(pointer.rectTransform.anchoredPosition.x, selectedButton[selectedOption].targetGraphic.rectTransform.anchoredPosition.y);
    }
    public void OnPointerDown()
    {
        pressed = true;
    }
    public void OnPointerUp()
    {
        pressed = false;
    }

    private void Update()
    {
        if (pressed)
        {
            filler.fillAmount += Time.deltaTime * 0.5f;
        }
        else
        {
            filler.fillAmount -= Time.deltaTime * 1f;
        }
    }
    
}
