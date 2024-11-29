using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [TextArea(1, 3)]
    public string[] _questRepeatLine;
    [SerializeField] private NPC_Quest _NPC_Quest;
    private void Start()
    {
        _NPC_Quest = GetComponent<NPC_Quest>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _isTalkable = true;
            _dialogueBox_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320);//Adjusting the size of the dialog box
            _text_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320);
            _questionText.text = "ç¢Ç¡ÇΩÇ»...";//Change text
            DialogManager.instance._NPC_Quest = GetComponent<NPC_Quest>();//let Player get quest detail from this Npc
            Zoom.instance._NPC = this.gameObject;
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
            Zoom.instance._NPC = null;
        }
    }

    private void Update()
    {
        //Player is within NPC range, conversation has not started, tap the screen
        if (_isTalkable && _dialogueBox_talking.activeInHierarchy == false &&DialogManager.instance._waitTimeController == true)
        {
        
            if(Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    StartCoroutine(Zoom.instance.ZoomIn());
                }
                
            }        
            if (Zoom.instance._isDone == true)
            {
                if (DialogManager.instance.CheckQuestStatus() == "Accepted" || DialogManager.instance.CheckQuestStatus() == "Waiting")
                {
                    DialogManager.instance.ShowDialogue(_line);//Show dialogue box
                }
                else if (DialogManager.instance.CheckQuestStatus() == "Completed")
                {
                    DialogManager.instance.ShowDialogue(_questCompletedLine);
                    DialogManager.instance._isCompleted = true;
                    _NPC_Quest.QuestDetail._questStatus = QuestDetail.QuestStatus.Repeat;
                }
                else if (DialogManager.instance.CheckQuestStatus() == "Repeat")
                {
                    DialogManager.instance.ShowDialogue(_questRepeatLine);
                    DialogManager.instance._isRepeat = true;
                }
                
                Zoom.instance._isDone = false;
            }
        }
    }
}
