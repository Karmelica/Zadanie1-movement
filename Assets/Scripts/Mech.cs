using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Mech : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private DirectionSlider directionSlider;

    static public int resource;
    private Image buttonImage;

    private float r = 0;
    private float targetAngle;
    private float rot = 1f;
    static public bool forward = true;
    private bool zero;

    [Header("Components")]
    public Image core;
    public List<Image> coreSlots = new();
    public List<int> slotEmpty = new() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
    public Canvas canvas;

    public Sprite forwardImage;
    public Sprite backImage;

    public Slider rightSlider;
    public Slider leftSlider;
    public Slider clutch;
    private Rigidbody rb;
    public Transform orientation;

    [Header("Overcharge")]

    private bool leftIsOvercharged;
    private bool rightIsOvercharged;
    //private bool tankEmpty = false;
    public GameObject overchargeText;
    public Image fuelImage;
    public Image fuelImageBack;
    public Image leftOvercharge;
    public Image rightOvercharge;
    private Collider colli;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            colli = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            colli = null;
        }
    }

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

    //private void LeftOverchargeDelete(){}

    //private void RightOverchargeDelete(){}

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

    private void Fuel()
    {
        if(fuelImage.fillAmount > 0)
        {
            fuelImage.fillAmount -= Time.deltaTime * 0.01f * ((4 + clutch.value) / 4);
            fuelImageBack.fillAmount = fuelImage.fillAmount;
        }
        else
        {
            clutch.value = 0;
        }
    }

    private void Overcharge()
    {
        if(!leftIsOvercharged)
        {
            if(leftOvercharge.fillAmount < 1)
            {
                leftOvercharge.fillAmount += Time.deltaTime * 0.01f * (leftSlider.value - 2);
            }
            else
            {
                leftIsOvercharged = true;
            }
        }
        else
        {
            if (leftSlider.value > 2)
            {
                leftSlider.value = 2;
            }
        }


        if (!rightIsOvercharged)
        {
            if (rightOvercharge.fillAmount < 1)
            {
                rightOvercharge.fillAmount += Time.deltaTime * 0.01f * (rightSlider.value - 2);
            }
            else
            {
                rightIsOvercharged = true;
            }
        }
        else
        {
            if(rightSlider.value > 2)
            {
                rightSlider.value = 2;
            }
        }
    }

    public void AddCore()
    {
        int rand;
        do
        {
            rand = Random.Range(0, 12);

            if (slotEmpty.Contains(rand))
            {
                slotEmpty.Remove(rand);
                Instantiate(core, coreSlots[rand].rectTransform, false);
                break;
            }
        } while (slotEmpty.Count > 0);
    }
    public void PickUpAction()
    {
        if (colli != null && slotEmpty.Count > 0)
        {
            AddCore();
            Destroy(colli.gameObject);
        }
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

        Fuel();
    }
}
