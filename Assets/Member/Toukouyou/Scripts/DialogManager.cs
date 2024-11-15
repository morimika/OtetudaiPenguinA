using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

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
    [SerializeField] private GameObject _cutIn;
    [SerializeField] private Transform _cutInPos;
    public NPC_Quest _NPC_Quest;

    public move _move;//change this to the player movement controll script later

    public bool _waitTimeController = true;
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
        CheckQuestStatus();
        //Debug.Log(CheckQuestStatus());
        if (_dialogueBox_talking.activeInHierarchy)//If there is a dialog box
        {
            _waitTimeController = false;
            _move.enabled = false;//disable player movement
            if (Input.GetMouseButtonDown(0))
            {
               if (_isScrolling == false)//If the scrolling is over
               {
                    _currentDialogueLine++;//The next text
                    if (_currentDialogueLine <= _dialogueLine.Length - 1)
                    {
                        //CheckName();//話し手の表示
                        //_dialogueText.text = _dialogueLine[_currentDialogueLine];
                        StartCoroutine("ScrollingText");
                    }
                    else
                    {
                        _dialogueBox_talking.SetActive(false);//Eliminate the dialog box when the conversation is over
                        _move.enabled = true;
                        _NPC_Quest.delegateQuest(); 

                        StartCoroutine(WaitTime());
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
    }

    public void ShowDialogue(string[] _newLine)
    {
        _dialogueLine = _newLine;
        _currentDialogueLine = 0;//Start the conversation from the beginning
        //CheckName();//話し手の表示
        _dialogueBox_talking.SetActive(true); //Show talking dialog box
        _dialogueBox.SetActive(false);
        StartCoroutine("ScrollingText");
    }

    //public void CheckName()
    //{
    //    if (_dialogueLine[_currentDialogueLine].StartsWith("Name-"))
    //    {
    //        _name.text = _dialogueLine[_currentDialogueLine].Replace("Name-", "");
    //        _currentDialogueLine++;//名前表示するための会話文を略す
    //    }
    //}

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
                case QuestDetail.QuestStatus.Completed:
                    if(Player_QuestList.instance.questList[i]._questName == _NPC_Quest.QuestDetail._questName)
                    {
                        return "Completed";
                    }
                    break;
                case QuestDetail.QuestStatus.Accepted:
                    if (Player_QuestList.instance.questList[i]._questName == _NPC_Quest.QuestDetail._questName)
                    {
                        return "Accepted";
                    }
                    break;
            }
        }  
        return "Waiting";// return Waiting as default
    }
    public void ShowCutIn()
    {
        Instantiate(_cutIn, _cutInPos);
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1.0f);
        _dialogueBox.SetActive(true);
        _waitTimeController = true;
    }
}
