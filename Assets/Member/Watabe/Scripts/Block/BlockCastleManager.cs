using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class BlockCastleManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject FalseImage;
    [SerializeField] private GameObject SuccessImage;
    [SerializeField] private GameObject ClearImage;
    [SerializeField] private GameObject Question1;
    [SerializeField] private GameObject Question2;
    [SerializeField] private GameObject Question3;

    public List<Button> FalseButton;
    public List<Button> SuccessButton;

    private int successButtonIndex = 0;
    private bool isInteractionDisabled = false;

    private void Start()
    {
        // 最初にQuestion1だけを表示
        Question1.SetActive(true);
        Question2.SetActive(false);
        Question3.SetActive(false);
        FalseImage.SetActive(false);
        SuccessImage.SetActive(false);
        ClearImage.SetActive(false);

        // FalseButtonにイベントを設定
        foreach (var button in FalseButton)
        {
            button.onClick.AddListener(() => { if (!isInteractionDisabled) ShowFalseImage(); });
        }

        // SuccessButtonにイベントを設定
        for (int i = 0; i < SuccessButton.Count; i++)
        {
            int index = i; // ローカル変数にキャプチャ
            SuccessButton[i].onClick.AddListener(() => { if (!isInteractionDisabled) HandleSuccessButton(index); });
        }
    }

    private async void ShowFalseImage()
    {
        DisableInteraction();
        FalseImage.SetActive(true);
        await UniTask.Delay(3000); // 4秒待つ
        FalseImage.SetActive(false);
        EnableInteraction();
    }

    private async void HandleSuccessButton(int index)
    {
        if (index == successButtonIndex)
        {
            DisableInteraction();

            if (index == 0)
            {
                // Question1 -> Question2
                SuccessImage.SetActive(true);
                await UniTask.Delay(3000); // 3秒間表示
                SuccessImage.SetActive(false);

                Question1.SetActive(false);
                Question2.SetActive(true);
            }
            else if (index == 1)
            {
                // Question2 -> Question3
                SuccessImage.SetActive(true);
                await UniTask.Delay(3000); // 3秒間表示
                SuccessImage.SetActive(false);

                Question2.SetActive(false);
                Question3.SetActive(true);
            }
            else if (index == 2)
            {
                // Question3 -> ClearImage
                SuccessImage.SetActive(true);
                await UniTask.Delay(3000); // 3秒間表示
                SuccessImage.SetActive(false);

                Question3.SetActive(false);
                ClearImage.SetActive(true);
                await UniTask.Delay(4000); // 4秒待つ
                SceneManager.LoadScene("CloudMiniGame");
            }

            successButtonIndex++;
            EnableInteraction();
        }
    }

    private void DisableInteraction()
    {
        isInteractionDisabled = true;
        foreach (var button in FalseButton) button.interactable = false;
        foreach (var button in SuccessButton) button.interactable = false;
    }

    private void EnableInteraction()
    {
        isInteractionDisabled = false;
        foreach (var button in FalseButton) button.interactable = true;
        foreach (var button in SuccessButton) button.interactable = true;
    }
}
