using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SceneStart_W : MonoBehaviour
{
    public GameObject movingObject; // 移動させるオブジェクト
    public Transform targetPosition; // 目的地のTransform
    public TextMeshProUGUI countdownText; // カウントダウン表示用のTextMeshPro
    public GameObject displayObject; // 1秒表示するオブジェクト
    public DragAndDrop dragAndDropScript; // DragAndDropスクリプトの参照

    //mori
    [SerializeField]
    private bool _doCount = false;

    async void Start()
    {
        // DragAndDropスクリプトを無効化
        if (dragAndDropScript != null)
            dragAndDropScript.enabled = false;

        // 初期設定
        countdownText.gameObject.SetActive(false); // カウントダウンを非表示
        displayObject.SetActive(false); // 表示オブジェクトを非表示

        // ゲーム開始フローを実行
        await MoveObjectToTargetAsync();
        if (_doCount)
        {
            await ShowCountdownAsync();
        }
        await ShowDisplayObjectAsync();

        // DragAndDropスクリプトを再び有効化
        if (dragAndDropScript != null)
            dragAndDropScript.enabled = true;
    }

    private async UniTask MoveObjectToTargetAsync()
    {
        await UniTask.Delay(3000);
        movingObject.transform.DOMove(targetPosition.position, 1.5f);
        await UniTask.Delay(1500);
    }

    private async UniTask ShowCountdownAsync()
    {
        countdownText.gameObject.SetActive(true); // カウントダウンを表示

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // カウントダウンの数値を更新
            await UniTask.Delay(1000);
        }

        countdownText.gameObject.SetActive(false); // カウントダウンを非表示
    }

    private async UniTask ShowDisplayObjectAsync()
    {
        displayObject.SetActive(true); // オブジェクトを表示
        await UniTask.Delay(1000); // 1秒待機
        displayObject.SetActive(false); // オブジェクトを非表示
    }
}
