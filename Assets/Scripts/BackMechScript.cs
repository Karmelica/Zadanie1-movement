using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackMechScript : MonoBehaviour
{
    public Mech mechScript;
    private RectTransform currentCore = null;
    public Button leftCoreButton;
    public Button rightCoreButton;

    private IEnumerator coreManager(RectTransform current)
    {
        current.localScale = Vector3.one;
        current.gameObject.GetComponent<Button>().enabled = false;
        current.gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(3f);
        current.gameObject.GetComponent<Image>().color = Color.cyan;
        current.localPosition = Vector3.zero;
        current.localScale = Vector3.one * 0.7f;
        current.gameObject.GetComponent<Button>().enabled = true;
        current.gameObject.SetActive(false);
    }

    public void GetThisImage(RectTransform buttonCore)
    {
        currentCore = buttonCore;
    }

    public void PutInLeftCore(RectTransform coreTransform)
    {
        if(currentCore != null)
        {
            mechScript.LeftOverchargeDelete();
            currentCore.position = coreTransform.position;
            StartCoroutine(coreManager(currentCore));
            Destroy(leftCoreButton.gameObject);
            currentCore = null;
        }
    }
    public void PutInRightCore(RectTransform coreTransform)
    {
        if (currentCore != null)
        {
            mechScript.RightOverchargeDelete();
            currentCore.position = coreTransform.position;
            StartCoroutine(coreManager(currentCore));
            Destroy(rightCoreButton.gameObject);
            currentCore = null;
        }
    }
    public void RefillFuel(RectTransform coreTransform)
    {
        if (currentCore != null)
        {
            currentCore.position = coreTransform.position;
            StartCoroutine(coreManager(currentCore));
            mechScript.AddFuel();
            currentCore = null;
        }
    }
}
