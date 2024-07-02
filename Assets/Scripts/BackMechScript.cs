using UnityEngine;
using UnityEngine.UI;

public class BackMechScript : MonoBehaviour
{
    public Mech mechScript;
    public Button leftCoreButton;
    public Button rightCoreButton;
    public GameObject leftCore;
    public GameObject rightCore;
    public GameObject fuelCore;


    //public void GetThisImage(RectTransform buttonCore)
    //{
    //    currentCore = buttonCore;
    //}

    public void PutInLeftCore()
    {
        if(mechScript.CoreChecker())
        {
            mechScript.LeftOverchargeDelete();
            mechScript.CoreGiver().SetActive(false);
            Destroy(leftCoreButton);
            leftCore.SetActive(true);
        }
    }
    public void PutInRightCore()
    {
        if (mechScript.CoreChecker())
        {
            mechScript.RightOverchargeDelete();
            mechScript.CoreGiver().SetActive(false);
            Destroy(rightCoreButton);
            rightCore.SetActive(true);
        }
    }
    public void RefillFuel()
    {
        if (mechScript.CoreChecker() && !fuelCore.activeInHierarchy)
        {
            mechScript.AddFuel();
            mechScript.CoreGiver().SetActive(false);
            fuelCore.SetActive(true);
        }
    }
}
