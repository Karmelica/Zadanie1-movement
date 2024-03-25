using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ForwardButton : MonoBehaviour
{
    public Image image;
    public Sprite forwardSwitch, forwardSwitchHighlight;
    public Sprite backwardSwitch, backwardSwitchHighlight;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnMouseOver()
    {
        if (Mech.forward)
        {
            image.sprite = forwardSwitchHighlight;
        }
        else
        {
            image.sprite = backwardSwitchHighlight;
        }
    }

    public void OnMouseExit() {
        if (Mech.forward)
        {
            image.sprite = forwardSwitch;
        }
        else
        {
            image.sprite = backwardSwitch;
        }
    }
}