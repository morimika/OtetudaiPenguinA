using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool _isTalkable;
    [SerializeField] private GameObject _dialogueBox_talking;
    [SerializeField] private TextMeshProUGUI _questionText;
    [TextArea(1, 3)]
    public string[] _line;
    [SerializeField] private RectTransform _dialogueBox_rectTransform;
    [SerializeField] private RectTransform _text_rectTransform;

    [TextArea(1, 3)]
    public string[] _questCompletedLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _isTalkable = true;
            _dialogueBox_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320);//Adjusting the size of the dialog box
            _text_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320);
            _questionText.text = "À§¤Ã¤¿¤Ê...";//Change text
            DialogManager.instance._NPC_Quest = GetComponent<NPC_Quest>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTalkable = false;
            _dialogueBox_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);//Adjusting the size of the dialog box
            _text_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
            _questionText.text = "?";//Change text
            DialogManager.instance._NPC_Quest = null;
        }
    }

    private void Update()
    {
        //Player is within NPC range, conversation has not started, tap the screen
        if (_isTalkable && _dialogueBox_talking.activeInHierarchy == false &&DialogManager.instance._waitTimeController == true && Input.GetMouseButtonDown(0))
        {
            if (DialogManager.instance.CheckQuestStatus() == "Completed")
            {
                _line = _questCompletedLine;
            }
                DialogManager.instance.ShowDialogue(_line);//Show dialogue box
        }
    }
}
