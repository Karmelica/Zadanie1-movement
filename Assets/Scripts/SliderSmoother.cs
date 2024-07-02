using UnityEngine;
using UnityEngine.UI;

public class SliderSmoother : MonoBehaviour
{
    private Slider slider;
    float r = 0;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentSliderValue = slider.value;
        float target = Mathf.SmoothDamp(currentSliderValue, Mathf.RoundToInt(currentSliderValue), ref r, 0.05f);
        slider.value = target;
    }
}
