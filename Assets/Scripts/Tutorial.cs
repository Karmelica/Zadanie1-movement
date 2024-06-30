using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Tutorial Components")]
    public Button hideText;
    public Image fadeAway;
    public RawImage task5Camera;
    public Slider leftLever;
    public Slider rightLever;
    public Slider clutchLevel;
    public Button pickUpButton;
    public Button forwardButton;
    public Button fuelCoreButton;
    public Button overchargeFillButton1;
    public Button overchargeFillButton2;
    public DirectionSlider dirSlider;

    private Color alpha;
    public Transform end;

    //bools that represent if task is complete (or set of tasks)
    public List<bool> task1;
    public bool task2;
    public List<bool> task3;
    public bool task4;
    public bool task5;
    public bool task5_2;
    public bool task6;
    public bool task7;
    public bool task8;

    [Header("Text")] //text which tells player what to do
    public List<TextMeshProUGUI> task1Text;
    public TextMeshProUGUI task2Text;
    public List<TextMeshProUGUI> task3Text;
    public TextMeshProUGUI task4Text;
    public TextMeshProUGUI task5Text;
    public TextMeshProUGUI task5_2Text;
    public TextMeshProUGUI task6Text;
    public TextMeshProUGUI task7Text;
    public List<TextMeshProUGUI> task8Text;

    public GameObject taskObject;

    [Header("Doors")] //obstacles which are deleted after completing a task
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door5;
    public GameObject door5_2;
    public GameObject door6;
    public GameObject door7;
    public GameObject door8;

    private void OnTriggerEnter(Collider other)
    {
        taskObject.SetActive(true);
        if (other.CompareTag("Task3"))
        {
            leftLever.value = 1;
            rightLever.value = 1;
            clutchLevel.value = 0;
            dirSlider.ResetRevMeter();
        }


        if (other.CompareTag("Task5"))
        {
            leftLever.value = 1;
            rightLever.value = 1;
            clutchLevel.value = 0;
            dirSlider.ResetRevMeter();

            forwardButton.gameObject.SetActive(true);
            task5Camera.gameObject.SetActive(true);
        }

        if (other.CompareTag("Task5.2"))
        {
            leftLever.value = 1;
            rightLever.value = 1;
            clutchLevel.value = 0;
            dirSlider.ResetRevMeter();

            task5Camera.gameObject.SetActive(false);
        }

        if (other.CompareTag("Task7"))
        {
            leftLever.value = 1;
            rightLever.value = 1;
            clutchLevel.value = 0;
            dirSlider.ResetRevMeter();

            fuelCoreButton.enabled = true;
        }

        if (other.CompareTag("Task8"))
        {
            task8Text[0].gameObject.SetActive(true);

            leftLever.value = 1;
            rightLever.value = 1;
            clutchLevel.value = 0;
            dirSlider.ResetRevMeter();

            overchargeFillButton1.enabled = true;
        }

        if (other.CompareTag("Coin"))
        {
            pickUpButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FadeAway"))
        {
            float distance = (transform.position - end.position).magnitude;
            alpha.a = fadeAway.color.a;
            alpha.a = 1 - (distance / 80f);
            fadeAway.color = alpha;

            if(distance < 5)
            {
                hideText.gameObject.SetActive(false);
                SceneManager.LoadScene(1);
            }
        }

        if (other.CompareTag("Task1"))
        {
            if(leftLever.value > 2)
            {
                leftLever.value = 2;
            }
            if (rightLever.value > 2)
            {
                rightLever.value = 2;
            }
            if (clutchLevel.value > 1)
            {
                clutchLevel.value = 1;
            }

            foreach (var text in task1Text)
            {
                text.gameObject.SetActive(true);
            }
            if (Task1())
            {
                other.gameObject.SetActive(false);
                foreach (var text in task1Text)
                {
                    text.gameObject.SetActive(false);
                }
                door1.SetActive(false);
            }
        }

        if (other.CompareTag("Task2"))
        {
            if (leftLever.value != rightLever.value)
            {
                Time.timeScale = 0.01f;
            }
            else
            {
                Time.timeScale = 1;
            }

            task2Text.gameObject.SetActive(true); //shows text in task area
            if (Task2()) //it task is completed
            {
                if(Time.timeScale != 1)
                {
                    Time.timeScale = 1;
                }
                other.gameObject.SetActive(false); //disables task are
                task2Text.gameObject.SetActive(false); //disables task text
                door2.SetActive(false); //opens obstacles
            }

        }

        if (other.CompareTag("Task3"))
        {
            foreach (var text in task3Text)
            {
                text.gameObject.SetActive(true);
            }
            if (Task3())
            {
                other.gameObject.SetActive(false);
                foreach (var text in task3Text)
                {
                    text.gameObject.SetActive(false);
                }
                door3.SetActive(false);
            }
        }

        if (other.CompareTag("Task4"))
        {
            task4Text.gameObject.SetActive(true);
            if (Task4())
            {
                other.gameObject.SetActive(false);
                task4Text.gameObject.SetActive(false);
            }
        }

        if (other.CompareTag("Task5"))
        {
            task5Text.gameObject.SetActive(true);
            if (Task5())
            {
                other.gameObject.SetActive(false);
                task5Text.gameObject.SetActive(false);
                door5.SetActive(false);
            }
        }

        if (other.CompareTag("Task5.2"))
        {
            leftLever.value = 1;
            rightLever.value = 1;
            clutchLevel.value = 0;

            task5Camera.gameObject.SetActive(false);
            task5_2Text.gameObject.SetActive(true);
            if (Task5_2())
            {
                other.gameObject.SetActive(false);
                task5_2Text.gameObject.SetActive(false);
                door5_2.SetActive(false);
            }
        }

        if (other.CompareTag("Task6"))
        {
            task6Text.gameObject.SetActive(true);
            if (task6)
            {
                other.gameObject.SetActive(false);
                task6Text.gameObject.SetActive(false);
                door6.SetActive(false);
            }
        }

        if (other.CompareTag("Task7"))
        {
            task7Text.gameObject.SetActive(true);
            if (task7)
            {
                other.gameObject.SetActive(false);
                task7Text.gameObject.SetActive(false);
                door7.SetActive(false);
            }
        }

        if (other.CompareTag("Task8"))
        {
            if (task8)
            {
                other.gameObject.SetActive(false);
                task8Text[1].gameObject.SetActive(false);
                door8.SetActive(false);
            }
        }
    }

    public void HideTutorial()
    {
        if(taskObject.activeInHierarchy)
        {
            taskObject.SetActive(false);
        }
        else
        {
            taskObject.SetActive(true);
        }
    }

    public bool Task1()
    {
        if (rightLever.value >= 2)
        {
            task1Text[0].fontStyle = FontStyles.Strikethrough;
            task1[0] = true;
        }

        if (leftLever.value >= 2)
        {
            task1Text[1].fontStyle = FontStyles.Strikethrough;
            task1[1] = true;
        }

        if (clutchLevel.value >= 1)
        {
            task1Text[2].fontStyle = FontStyles.Strikethrough;
            task1[2] = true;
        }

        foreach (var task in task1)
        {
            if (task == false)
            {
                return false;
            }
        }

        return true;
    }

    private bool Task2()
    {
        if (leftLever.value > 2 && rightLever.value > 2)
        {
            task2 = true; //changes bool to true after finishing task
        }

        return task2; //returning true after completion
    }

    public bool Task3()
    {
        if (rightLever.value == 1)
        {
            task3Text[0].fontStyle = FontStyles.Strikethrough;
            task3[0] = true;
        }

        if (leftLever.value > 1)
        {
            task3Text[1].fontStyle = FontStyles.Strikethrough;
            task3[1] = true;
        }

        if (clutchLevel.value >= 1)
        {
            task3Text[2].fontStyle = FontStyles.Strikethrough;
            task3[2] = true;
        }

        foreach (var task in task3)
        {
            if (task == false)
            {
                return false;
            }
        }

        return true;
    }
    private bool Task4()
    {
        if (transform.position.x >= 46)
        {
            task4 = true; //changes bool to true after finishing task
        }

        return task4; //returning true after completion
    }

    private bool Task5()
    {
        if (!Mech.forward)
        {
            task5 = true;
        }

        return task5;
    }

    private bool Task5_2()
    {
        if (Mech.forward)
        {
            task5_2 = true;
        }

        return task5_2;
    }

    public void Task6Done()
    {
        task6 = true;
    }

    public void Task7Done()
    {
        task7 = true;
    }

    public void ShowNextCore()
    {
        task8Text[0].enabled = false;
        overchargeFillButton2.enabled = true;
        task8Text[1].enabled = true;
    }

    public void Task8Done()
    {
        task8 = true;
    }

    private void Start()
    {
        overchargeFillButton2.enabled = false;
        overchargeFillButton1.enabled = false;
        fuelCoreButton.enabled = false;

        forwardButton.gameObject.SetActive(false);
        hideText.gameObject.SetActive(true);
    }
}
