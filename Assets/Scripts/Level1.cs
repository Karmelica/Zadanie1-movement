using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public TextMeshProUGUI coresCountText;
    public GameObject pauseMenu;
    public Button exitButton;

    private int currentZone;
    private int door1Req = 0;
    private int door2Req = 0;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    private void OnTriggerExit(Collider other)
    {
        currentZone = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Task1"))
        {
            currentZone = 1;
        }
        if (other.CompareTag("Task2"))
        {
            currentZone = 2;
        }
    }

    public void Lvl1()
    {
        Mech.coresNum++;
        coresCountText.text = Mech.coresNum.ToString() + "/" + "11";
        if(currentZone == 1)
        {
            door1Req++;
            if(door1Req == 2)
            {
                door1.SetActive(false);
            }
        }
        if(currentZone == 2)
        {
            door2Req++;
            if(door2Req == 2)
            {
                door2.SetActive(false);
            }
        }

        if(door1Req == 2 || door2Req == 2)
        {
            door3.SetActive(false);
        }

        if (Mech.coresNum >= 11)
        {
            exitButton.gameObject.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
}
