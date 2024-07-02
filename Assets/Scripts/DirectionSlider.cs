using UnityEngine;
using UnityEngine.UI;

public class DirectionSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Sprite[] revImages;
    public AudioClip[] engineSounds;
    public AudioSource enginePlayer;

    public Image revMeter;

    public void ResetRevMeter()
    {
        revMeter.sprite = revImages[0];
    }

    public void GearUp()
    {
        slider.value++;
        revMeter.sprite = revImages[(int)slider.value];
        enginePlayer.Stop();
        enginePlayer.PlayOneShot(engineSounds[(int)slider.value]);
    }

    public void GearDown()
    {
        slider.value--;
        revMeter.sprite = revImages[(int)slider.value];
        if(slider.value != 0)
        {
            enginePlayer.Stop();
            enginePlayer.PlayOneShot(engineSounds[(int)slider.value]);
        }
    }

    private void Start()
    {
        revMeter.sprite = revImages[(int)slider.value];
    }
}
