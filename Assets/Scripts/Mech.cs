using System.Collections;
using System.Data;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mech : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private DirectionSlider directionSlider;

    private Image buttonImage;

    private float r = 0;
    private float targetAngle;
    private float rot = 1f;
    static public bool forward = true;
    private bool zero;

    [Header("Components")]

    public Sprite forwardImage;
    public Sprite backImage;

    public Slider rightSlider;
    public Slider leftSlider;
    public Slider clutch;
    private Rigidbody rb;
    public Transform orientation;

    [Header("Overcharge")]

    private bool isOvercharged = false;
    public GameObject overchargeText;
    public Image overchargeImage;
    public float overchargeLevel = 0f;

    public IEnumerator Walking()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f - (leftSlider.value + rightSlider.value) / 8f);

            rot = leftSlider.value - rightSlider.value;

            if (leftSlider.value == 1 && rightSlider.value == 1)
            {
                zero = true;
            }
            else
            {
                zero = false;
            }

            if (!zero && !(rightSlider.value == 1 || leftSlider.value == 1))
            {
                if (forward)
                {
                    rb.AddForce((6f * clutch.value) * orientation.forward, ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce((-6f * clutch.value) * orientation.forward, ForceMode.Impulse);
                }
            }

            if (rot != 0f && clutch.value != 0)
            {
                if (forward)
                {
                    targetAngle += (5 * rot);
                }
                else
                {
                    targetAngle -= (5 * rot);
                }
            }

        }
    }

    public void MoveForward()
    {
        clutch.value = 0;
        directionSlider.Reset();
        forward = !forward;

        if(rightSlider.value > leftSlider.value)
        {
            leftSlider.value = rightSlider.value;
        }
        else
        {
            rightSlider.value = leftSlider.value;
        }

        if (forward)
        {
            buttonImage.sprite = forwardImage;
        }
        else
        {
            buttonImage.sprite = backImage;
        }
    }

    private void Overcharge()
    {
        if (clutch.value > 2 && !isOvercharged && !zero)
        {
            overchargeLevel += Time.deltaTime * 0.02f * clutch.value;
        }
        else if (clutch.value <= 2 && overchargeLevel > 0)
        {
            overchargeLevel -= Time.deltaTime * 0.1f * (2 - clutch.value);
        }

        if (overchargeLevel >= 1f)
        {
            Overcharged();
        }
        if (isOvercharged && overchargeLevel <= 0)
        {
            overchargeText.SetActive(false);
            isOvercharged = false;
            StartCoroutine(Walking());
        }
    }

    private void Overcharged()
    {
        overchargeText.SetActive(true);
        isOvercharged = true;
        StopAllCoroutines();
        clutch.value = 0;
        leftSlider.value = 0;
        rightSlider.value = 0;
        directionSlider.Reset();
        forward = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetAngle = transform.eulerAngles.y;

        rb = GetComponent<Rigidbody>();
        buttonImage = button.GetComponent<Image>();

        StartCoroutine(Walking());
    }


    private void Update()
    {
        float angleChange = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref r, 0.4f);
        transform.rotation = Quaternion.Euler(transform.rotation.x, angleChange, transform.rotation.z);

        Overcharge();

        overchargeImage.fillAmount = overchargeLevel;

    }
}
