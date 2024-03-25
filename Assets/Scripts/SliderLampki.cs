using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderLampki : MonoBehaviour
{
    public Sprite[] lampki;
    public Slider slider;
    public Image lampkiImage;

    private void Start()
    {
        lampkiImage.sprite = lampki[(int)slider.value - 1];
    }

    public void OnSliderChange()
    {
        lampkiImage.sprite = lampki[(int)slider.value - 1];
    }
}
