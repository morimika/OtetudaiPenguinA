using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private Button successButton;
    [SerializeField] private Button falseButton;
    [SerializeField] private GameObject successObject;
    [SerializeField] private GameObject falseObject;

    private void Start()
    {
        successObject.SetActive(false);
        falseObject.SetActive(false);

        successButton.onClick.AddListener(OnSuccessButtonClicked);
        falseButton.onClick.AddListener(OnFalseButtonClicked);
    }

    private async void OnSuccessButtonClicked()
    {

        successObject.SetActive(true);

        await UniTask.Delay(5000);

        SceneManager.LoadScene("CloudMiniGame");
    }

    private async void OnFalseButtonClicked()
    {
        falseObject.SetActive(true);

        await UniTask.Delay(5000);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
