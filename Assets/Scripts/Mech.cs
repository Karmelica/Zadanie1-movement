using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool anyCoreSlotLeft = false;

    [Header("Components")]
    public Image core;
    public List<Image> cores;
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
    public Image fuelImage;
    public Image fuelImageBack;
    public Image leftOverchargeLevel;
    public Image rightOverchargeLevel;
    private Collider colli;
    private bool hasRightCore = false;
    private bool hasLeftCore = false;

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


    public void MoveForward()
    {
        clutch.value = 0;
        directionSlider.Reset();
        forward = !forward;

        if (rightSlider.value > leftSlider.value)
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
        if (fuelImage.fillAmount > 0)
        {
            fuelImage.fillAmount -= Time.deltaTime * 0.01f * ((4 + clutch.value) / 4);
            fuelImageBack.fillAmount = fuelImage.fillAmount;
        }
        else
        {
            clutch.value = 0;
        }
    }

    public void AddFuel()
    {
        fuelImage.fillAmount += 0.3f;
    }
    public void LeftOverchargeDelete()
    {
        leftOverchargeLevel.fillAmount = 0;
        hasLeftCore = true;
    }

    public void RightOverchargeDelete()
    {
        rightOverchargeLevel.fillAmount = 0;
        hasRightCore = true;
    }

    private void Overcharge()
    {
        if (!hasLeftCore) {
            if (leftOverchargeLevel.fillAmount < 1)
            {
                leftOverchargeLevel.fillAmount += Time.deltaTime * 0.01f * (leftSlider.value - 2);
            }
            else
            {
                if (leftSlider.value > 2)
                {
                    leftSlider.value = 2;
                }
            }
        }
        
        if(!hasRightCore) {
            if (rightOverchargeLevel.fillAmount < 1)
            {
                rightOverchargeLevel.fillAmount += Time.deltaTime * 0.01f * (rightSlider.value - 2);
            }
            else
            {
                if (rightSlider.value > 2)
                {
                    rightSlider.value = 2;
                }
            }
        }
        
    }

    public void AddCore()
    {
        int rand;
        while (anyCoreSlotLeft)
        {
            rand = Random.Range(0, cores.Count);

            if (!cores[rand].gameObject.activeInHierarchy)
            {
                cores[rand].gameObject.SetActive(true);
                break;
            }
        }
    }

    public bool CoreChecker()
    {
        foreach (var core in cores)
        {
            if (core.gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    public GameObject CoreGiver()
    {
        foreach (var core in cores)
        {
            if (core.gameObject.activeInHierarchy)
            {
                return core.gameObject;
            }
        }
        return null;
    }

    public void PickUpAction()
    {
        if (colli != null)
        {
            foreach (var core in cores)
            {
                if (core.gameObject.activeSelf)
                {
                    anyCoreSlotLeft = false;
                }
                else
                {
                    anyCoreSlotLeft = true;
                    AddCore();
                    Destroy(colli.gameObject);
                    break;
                }
            }
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
