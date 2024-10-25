using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ObjectOverlapManager : MonoBehaviour
{
    private int currentValue = 7;
    public TextMeshProUGUI valueText;
    public List<GameObject> objectsToAdd3;
    public List<GameObject> objectsToAdd4;
    public List<GameObject> objectsToAdd5;
    public GameObject successObject;
    public GameObject failureObject;
    public Button resetButton;

    void Start()
    {
        UpdateValueText();
        successObject.SetActive(false);
        failureObject.SetActive(false);

        // リセットボタンにリセットメソッドを追加
        resetButton.onClick.AddListener(ResetValue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (objectsToAdd3.Contains(other.gameObject))
        {
            currentValue += 3;
        }
        else if (objectsToAdd4.Contains(other.gameObject))
        {
            currentValue += 4;
        }
        else if (objectsToAdd5.Contains(other.gameObject))
        {
            currentValue += 5;
        }

        UpdateValueText();

        if (currentValue == 15)
        {
            ShowSuccess();
        }
        else if (currentValue > 15)
        {
            ShowFailure();
        }
    }

    private void ShowSuccess()
    {
        successObject.SetActive(true); // 成功オブジェクトを表示
        resetButton.gameObject.SetActive(false); // リセットボタンを非表示
    }

    private void ShowFailure()
    {
        failureObject.SetActive(true); // 失敗オブジェクトを表示
        resetButton.gameObject.SetActive(false); // リセットボタンを非表示
        StartCoroutine(RestartAfterDelay(5f)); // 5秒後にリスタート
    }

    private void UpdateValueText()
    {
        valueText.text = "Current Value: " + currentValue.ToString();
    }

    private void ResetValue()
    {
        currentValue = 7;
        UpdateValueText();
        successObject.SetActive(false);
        failureObject.SetActive(false);
        resetButton.gameObject.SetActive(true);
    }

    private IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
