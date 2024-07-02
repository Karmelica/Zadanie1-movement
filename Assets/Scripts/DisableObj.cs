using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisableObj : MonoBehaviour
{
    [SerializeField] private Image coreBar;

    private IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        coreBar.fillAmount = 1;
        StartCoroutine(TurnOff());
    }

    private void Update()
    {
        coreBar.fillAmount -= Time.deltaTime * 0.33f;
    }
}
