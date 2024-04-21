using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public Collider colli;

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

    public void PickUpAction()
    {
        if (colli != null)
        {
            Mech.resource++;
            Destroy(colli.gameObject);
        }
    }
}
