using UnityEngine;
using UnityEngine.UI;

public class OnMouseOv : MonoBehaviour
{
    public Sprite mouseAway;
    public Sprite mouseOver;
    private Image currentImage;

    private void Start()
    {
        currentImage = GetComponent<Image>();
    }

    public void OnMouseOver()
    {
        currentImage.sprite = mouseOver;
    }

    public void OnMouseExit()
    {
        currentImage.sprite = mouseAway;
    }
}
