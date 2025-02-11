using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class PocketLongTap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float time = 0;
    private bool isDown = false;
    private float longTapTime = 1.0f;

    [SerializeField] private Image circleImage;

    [SerializeField]
    private PocketButton pocketButton;
    [SerializeField]
    private PlaySceneDatas playSceneDatas;

    /// <summary>
    /// âüâ∫ÇÕÇ∂Çﬂ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        playSceneDatas.TapType = PlaySceneTapType.Pose;
        isDown = true;
        time = 0f;
        circleImage.fillAmount = 0f;
    }

    /// <summary>
    /// âüâ∫èIÇÌÇË
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        circleImage.fillAmount = 0f;
        playSceneDatas.TapType = PlaySceneTapType.Play;
    }


    void Update()
    {
        //âüâ∫ÇµÇƒÇ¢ÇÈèÍçá
        if (isDown)
        {
            time += Time.deltaTime;
            //éûä‘ÇñûÇΩÇµÇΩ
            if (time >= longTapTime)
            {
                pocketButton.OpenPocket();
                Debug.Log("Long Tap");
                isDown = false;
            }
            circleImage.fillAmount = time / longTapTime;
        }
    }
}