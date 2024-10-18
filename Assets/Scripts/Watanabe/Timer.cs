using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Timer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image uiFill;
    [SerializeField] private float CountTime;

    private void Update()
    {
        float timer = CountTime - Time.time;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        uiFill.fillAmount = Mathf.InverseLerp(0, CountTime, timer);
    }
}