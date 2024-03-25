using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Sprite[] revImages;
    public Image revMeter;

    public void Reset()
    {
        revMeter.sprite = revImages[0];
    }

    public void GearUp()
    {
        slider.value++;
        revMeter.sprite = revImages[(int)slider.value];
    }

    public void GearDown()
    {
        slider.value--;
        revMeter.sprite = revImages[(int)slider.value];
    }

    private void Start()
    {
        revMeter.sprite = revImages[(int)slider.value];
    }
}
