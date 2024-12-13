using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

public class ObjectOverlapManager : MonoBehaviour
{
    private int currentValue = 7; // 初期値を7に設定
    public TextMeshProUGUI valueText; // 数値を表示するTextMeshProUGUI
    public List<GameObject> objectsToAdd3; // 3増加するオブジェクトリスト
    public List<GameObject> objectsToAdd4; // 4増加するオブジェクトリスト
    public List<GameObject> objectsToAdd5; // 5増加するオブジェクトリスト
    public GameObject successObject; // 成功時に表示するオブジェクト
    public GameObject failureObject; // 失敗時に表示するオブジェクト
    public Button resetButton; // リセットボタン
    public DragAndDrop dragAndDropScript; // DragAndDropスクリプトの参照

    public GameObject range7To10Object;
    public GameObject range10To13Object;
    public GameObject range13To15Object;
    public GameObject range15AndAboveObject;

    private HashSet<GameObject> overlappingObjects = new HashSet<GameObject>(); // 現在重なっているオブジェクトを追跡

    //mori
    [SerializeField,Label("プレイヤーの所持シールデータ")]
    private ItemList _playerSeal;
    [SerializeField,Label("プレイヤーに与えるシール")]
    private ItemData _giveSeal;
    [SerializeField]
    private GameObject _scissoersObj;

    void Start()
    {
        UpdateValueText();

        successObject.SetActive(false);
        failureObject.SetActive(false);

        resetButton.onClick.AddListener(ResetValue);

        UpdateRangeObjects();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!overlappingObjects.Contains(other.gameObject))
        {
            overlappingObjects.Add(other.gameObject);
            HandleOverlapAsync(other.gameObject).Forget();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        overlappingObjects.Remove(other.gameObject);
    }

    private async UniTaskVoid HandleOverlapAsync(GameObject overlappingObject)
    {
        //mori
        await UniTask.Delay(00);

        if (overlappingObjects.Contains(overlappingObject))
        {
            if (objectsToAdd3.Contains(overlappingObject))
            {
                currentValue += 3;
            }
            else if (objectsToAdd4.Contains(overlappingObject))
            {
                currentValue += 4;
            }
            else if (objectsToAdd5.Contains(overlappingObject))
            {
                currentValue += 5;
            }

            UpdateValueText();
            UpdateRangeObjects();

            if (currentValue == 15)
            {
                ShowSuccess();
            }
            else if (currentValue > 15)
            {
                ShowFailure();
            }
        }
    }

    // 成功オブジェクトを表示し、リセットボタンを非表示にする
    private void ShowSuccess()
    {
        // DragAndDropスクリプトを無効化
        if (dragAndDropScript != null)
            dragAndDropScript.enabled = false;
        successObject.SetActive(true);
        resetButton.gameObject.SetActive(false);
        //mori
        _playerSeal.items.Add(_giveSeal);
        PlayerSetPos.PlayerPos = new Vector2(-3.24f,-2.84f);
        Invoke(nameof(ChangeScenetoMain), 2f);
    }

    //mori
    private void ChangeScenetoMain()
    {
        SceneManager.LoadScene("Mori_MainGameScene");
    }

    // 失敗オブジェクトを表示し、リセットボタンを非表示にする
    private void ShowFailure()
    {
        // DragAndDropスクリプトを無効化
        if (dragAndDropScript != null)
            dragAndDropScript.enabled = false;
        failureObject.SetActive(true);
        //mori
        //resetButton.gameObject.SetActive(false);
        //StartCoroutine(RestartAfterDelay(5f));
    }

    // 現在の数値をTextに表示
    private void UpdateValueText()
    {
        valueText.text = currentValue.ToString();
    }

    private void ResetValue()
    {
        currentValue = 7;
        UpdateValueText();
        successObject.SetActive(false);
        failureObject.SetActive(false);
        resetButton.gameObject.SetActive(true);

        //mori
        _scissoersObj.gameObject.transform.position = new Vector2(3,-2.5f);
        dragAndDropScript.enabled = true;
        resetButton.onClick.AddListener(ResetValue);
        UpdateRangeObjects();
        // シーンを再ロードする
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // 範囲ごとのオブジェクトの表示を更新
        UpdateRangeObjects();
    }


    // 指定した秒数後にシーンを再ロードするコルーチン
    private IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 現在の数値に応じて範囲ごとのオブジェクトを表示/非表示
    private void UpdateRangeObjects()
    {
        range7To10Object.SetActive(currentValue > 7 && currentValue < 10);
        range10To13Object.SetActive(currentValue >= 10 && currentValue < 13);
        range13To15Object.SetActive(currentValue >= 13 && currentValue <= 15);
        range15AndAboveObject.SetActive(currentValue >= 16);
    }
}
