using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.UI;

public class MenuScript : MonoBehaviour
{
    private int selectedOption = 0;
    private bool pressed;
    private bool leftDragging;
    private bool rightDragging;
    private AsyncOperation asyncOperation;
    private float currentLoadValue;
    private float targetLoadValue;

    public Slider progressBar;
    public Slider leftSlider;
    public Slider rightSlider;
    public Slider volumeSlider;
    public GameObject loadingScreen;
    public GameObject options;
    public GameObject credits;
    public List<Button> selectedButton;
    public Button selector;
    public Image filler;
    public Image pointer;

    public void ChangeOption()
    {
        pointer.rectTransform.anchoredPosition = new Vector2(pointer.rectTransform.anchoredPosition.x, selectedButton[selectedOption].targetGraphic.rectTransform.anchoredPosition.y);
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("PlayerVolume", volumeSlider.value);
        AudioListener.volume = PlayerPrefs.GetFloat("PlayerVolume");
    }

    public void SliderChange()
    {
        if(!options.activeInHierarchy && !credits.activeInHierarchy)
        {
            if (leftSlider.value < -0.75f)
            {
                if (selectedOption < 4)
                {
                    leftSlider.interactable = false;
                    leftSlider.value = -0.75f;
                    selectedOption++;
                    ChangeOption();
                }
            }

            if (rightSlider.value > 0.75)
            {
                if (selectedOption > 0)
                {
                    rightSlider.interactable = false;
                    rightSlider.value = 0.75f;
                    selectedOption--;
                    ChangeOption();
                }
            }
        }
        else if (options.activeInHierarchy)
        {
            if (leftSlider.value == -1)
            {
                if (volumeSlider.value > 0)
                {
                    volumeSlider.value -= 0.1f;
                    leftSlider.value = 0;
                    leftSlider.interactable = false;
                }
            }

            if (rightSlider.value == 1)
            {
                if (volumeSlider.value < 1)
                {
                    volumeSlider.value += 0.1f;
                    rightSlider.value = 0;
                    rightSlider.interactable = false;
                }
            }
        }
    }

    private void MenuOption(int selected)
    {
        switch (selected)
        {
            case 0:
                Debug.Log("Not ready yet");    
                break;
            case 1:
                loadingScreen.SetActive(true);
                asyncOperation = SceneManager.LoadSceneAsync(0);
                asyncOperation.allowSceneActivation = false;
                break;
            case 2:
                options.SetActive(true);
                selectedOption = 5;
                break;
            case 3:
                credits.SetActive(true);
                selectedOption = 5;
                break;
            case 4:
                Application.Quit();
                break;
            case 5:
                credits.SetActive(false);
                options.SetActive(false);
                selectedOption = 0;
                ChangeOption();
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

    public void Drag(int side)
    {
        if (side == 0)
        {
            leftDragging = true;
        }
        else
        {
            rightDragging = true;
        }
    }

    public void EndDrag(int side)
    {
        if (side == 0)
        {
            leftDragging = false;
        }
        else
        {
            rightDragging = false;
        }
    }

    private void Update()
    {
        if(asyncOperation!= null)
        {
            targetLoadValue = asyncOperation.progress / 0.9f;

            currentLoadValue = Mathf.MoveTowards(currentLoadValue, targetLoadValue, 0.5f * Time.deltaTime);
            progressBar.value = currentLoadValue;
            if(Mathf.Approximately(progressBar.value, 1))
            {
                asyncOperation.allowSceneActivation = true;
            }
        }

        if (!leftDragging)
        {
            leftSlider.value += Time.deltaTime;
            if(leftSlider.value == 0)
            {
                leftSlider.interactable = true;
            }

        }
        if (!rightDragging)
        {
            rightSlider.value -= Time.deltaTime;
            if (rightSlider.value == 0)
            {
                rightSlider.interactable = true;
            }
        }

        if (pressed)
        {
            filler.fillAmount += Time.deltaTime * 0.5f;
        }
        else
        {
            filler.fillAmount -= Time.deltaTime * 1f;
        }

        if(filler.fillAmount >= 1f)
        {
            MenuOption(selectedOption);
            filler.fillAmount = 0;
        }
    }

    private void Start()
    {
        progressBar.value = currentLoadValue = targetLoadValue = 0;

        AudioListener.volume = PlayerPrefs.GetFloat("PlayerVolume");
        volumeSlider.value = PlayerPrefs.GetFloat("PlayerVolume");
    }

}
