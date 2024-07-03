using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
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

    private void MenuOption(int selected)
    {
        switch (selected)
        {
            case 0:
                SceneManager.LoadScene(0);
                break;
        }
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

        if (filler.fillAmount >= 1f)
        {
            MenuOption(selectedOption);
            filler.fillAmount = 0;
        }
    }
}
