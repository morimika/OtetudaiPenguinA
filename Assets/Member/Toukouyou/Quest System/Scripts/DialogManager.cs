using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _dialogueBox_talking;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    //[SerializeField] private TextMeshProUGUI _name;
     [TextArea(1, 3)]
    public string[] _dialogueLine;
    [SerializeField] private int _currentDialogueLine;//current conversation sentence
    private bool _isScrolling;//If the text is scrolling
    [SerializeField] private GameObject _startCutIn;
    [SerializeField] private GameObject _completedCutIn;
    [SerializeField] private Transform _cutInPos;
    public NPC_Quest _NPC_Quest;
    public RepeatQuestChoice _RepeatQuestChoice;

    public bool _waitTimeController = true;
    public bool _isCompleted;
    public bool _isRepeat;
    public Player _move;//!!!
                      //                                     change this to the player movement controll script later

    public bool _testClick = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (_dialogueBox_talking.activeInHierarchy)//If there is a dialog box
        {
            _move.enabled = false;
            _waitTimeController = false;
           
            if (Input.GetMouseButtonDown(0))
            {
               
                //if (EventSystem.current.IsPointerOverGameObject()) return;
                if (_isScrolling == false)//If the scrolling is over
               {
                    _currentDialogueLine++;//The next text
                    if (_currentDialogueLine <= _dialogueLine.Length - 1)
                    {
                        StartCoroutine("ScrollingText");
                    }
                    else
                    {
                        _dialogueBox_talking.SetActive(false);//Eliminate the dialog box when the conversation is over
                        StartCoroutine(Zoom.instance.ZoomOut());                                               
                    }
               }
                else
                {
                    //If it is scrolling,speed up
                    StopCoroutine("ScrollingText");
                    _dialogueText.text = _dialogueLine[_currentDialogueLine];
                    _isScrolling = false;
                }
            }
        }
        if (Zoom.instance._isZoomOut == true)
        {
            _NPC_Quest.delegateQuest();
            if (_isCompleted)
            {
                ShowCompletedCutIn();
                _isCompleted = false;
            }
            else if (_isRepeat)
            {
                _RepeatQuestChoice.ShowCanvas();
                _isRepeat = false;
            }
            Zoom.instance._isZoomOut = false;
            StartCoroutine(WaitTime());

        }
    }
    public void ShowDialogue(string[] _newLine)
    {
        _dialogueLine = _newLine;
        _currentDialogueLine = 0;//Start the conversation from the beginning
        _dialogueBox_talking.SetActive(true); //Show talking dialog box
        _dialogueBox.SetActive(false);
        StartCoroutine("ScrollingText");
    }
    private IEnumerator ScrollingText()
    {
        _isScrolling = true;//Begin scrolling
        _dialogueText.text = null;//Show text from the head of the dialog box
        foreach(char letter in _dialogueLine[_currentDialogueLine].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        _isScrolling = false;
    }
    public string CheckQuestStatus()
    {
        if(_NPC_Quest == null)//if Npc has no quest,return null
        {
            return "null";
        }
        for (int i = 0; i < Player_QuestList.instance.questList.Count;i++) 
        {
            switch(Player_QuestList.instance.questList[i]._questStatus)
            {
                case QuestDetail.QuestStatus.Accepted:
                    if (Player_QuestList.instance.questList[i]._questName == _NPC_Quest.QuestDetail._questName)
                    {
                        return "Accepted";
                    }
                    break;
                
                case QuestDetail.QuestStatus.Completed:
                    if(Player_QuestList.instance.questList[i]._questName == _NPC_Quest.QuestDetail._questName)
                    {
                        return "Completed";
                    }
                    break;

                case QuestDetail.QuestStatus.Repeat:
                    if (Player_QuestList.instance.questList[i]._questName == _NPC_Quest.QuestDetail._questName)
                    {
                        return "Repeat";
                    }
                    break;
            }
        }  
        return "Waiting";// return Waiting as default
    }
    public void ShowStartCutIn()
    {
        Instantiate(_startCutIn, _cutInPos);
    }
    public void ShowCompletedCutIn()
    {
        Instantiate(_completedCutIn, _cutInPos);
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2.5f);
        _dialogueBox.SetActive(true);
        _waitTimeController = true;
    }
}
