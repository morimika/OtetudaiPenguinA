using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool _isTalkable;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _dialogueBox_talking;
    [SerializeField] private TextMeshProUGUI _questionText;
    [TextArea(1, 3)]
    public string[] _line;
    [SerializeField] private RectTransform _dialogueBox_rectTransform;
    [SerializeField] private RectTransform _questionText_rectTransform;
    [TextArea(1, 3)]
    public string[] _questCompletedLine;
    [TextArea(1, 3)]
    public string[] _questRepeatLine;
    [SerializeField] private NPC_Quest _NPC_Quest;
    [SerializeField] private GameObject Player;
    private RaycastHit2D hit;
    private Ray ray;
    private void Start()
    {
        _NPC_Quest = GetComponent<NPC_Quest>();
    }
    private void Update()
    {
        if (CalculateDistance() <= 4 )
        {
            _isTalkable = true;
            _dialogueBox_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320);//Adjusting the size of the dialog box
            _questionText_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320);
            _questionText.text = "Help Me";//"¢‚Á‚½‚È...";//Change text
            DialogManager.instance._NPC_Quest = GetComponent<NPC_Quest>();//let Player get quest detail from this Npc
            Zoom.instance._NPC = this.gameObject;
        }
        else if (CalculateDistance() > 4 )
        {
            _isTalkable = false;
            _dialogueBox_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);//Adjusting the size of the dialog box
            _questionText_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
            _questionText.text = "?";//Change text
            DialogManager.instance._NPC_Quest = null;
            Zoom.instance._NPC = null;
        }

        //Player is within NPC range, conversation has not started, tap the screen
        if (_isTalkable && _dialogueBox.activeInHierarchy == true && _dialogueBox_talking.activeInHierarchy == false &&DialogManager.instance._waitTimeController == true && DialogManager.instance._testClick == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity).collider == null)
                {
                    return;
                }
                else
                {
                    hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                }
                if (hit.collider.gameObject.tag == "NPC")
                {
                    DialogManager.instance._testClick = false;
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        StartCoroutine(Zoom.instance.ZoomIn());
                    }
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
    public float CalculateDistance()
    {
        return Mathf.Sqrt(Mathf.Pow(this.transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(this.transform.position.y - Player.transform.position.y, 2));
    }
}
