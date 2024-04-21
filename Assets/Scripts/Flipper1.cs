using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper1 : MonoBehaviour
{
    public Animator animator;

    public void OnButtonClick()
    {
        animator.Play("Flip Reverse");
    }
}
