using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public Animator animator;

    public void OnButtonClick()
    {
        animator.Play("Flip");
    }
}
