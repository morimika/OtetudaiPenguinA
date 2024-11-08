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
    [SerializeField] private int _currentDialogueLine;//現在の会話文
    private bool _isScrolling;//テキストはスクロールしているかどうか
    [SerializeField] private GameObject _cutIn;
    [SerializeField] private Transform _cutInPos;
    public NPC_Quest _NPC_Quest;
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
        Debug.Log(CheckQuestStatus());
        if (_dialogueBox_talking.activeInHierarchy)//ダイアログボックスがあれば
        {
            if (Input.GetMouseButtonDown(0))
            {
               if (_isScrolling == false)//スクロールが終わったら
               {
                    _currentDialogueLine++;//次の会話文
                    if (_currentDialogueLine <= _dialogueLine.Length - 1)
                    {
                        //CheckName();//話し手の表示
                        //_dialogueText.text = _dialogueLine[_currentDialogueLine];
                        StartCoroutine("ScrollingText");
                    }
                    else
                    {
                        _dialogueBox_talking.SetActive(false);//会話が終わればダイアログボックスを無くす
                        _NPC_Quest.delegateQuest(); 
                        _dialogueBox.SetActive(true);                        
                    }
               }
                else
                {
                    //スクロールしていれば、加速する
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
        _currentDialogueLine = 0;//初めてから会話文を表示する
        //CheckName();//話し手の表示
        _dialogueBox_talking.SetActive(true); //ダイアログボックス表示
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
        _isScrolling = true;//スクロール開始
        _dialogueText.text = null;//一番最初のところから文字を表示するため
        foreach(char letter in _dialogueLine[_currentDialogueLine].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        _isScrolling = false;
    }

    public string CheckQuestStatus()
    {
        if(_NPC_Quest == null)
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
            //if (Player_QuestList.instance.questList[i]._questStatus == QuestDetail.QuestStatus.Completed
            //    && Player_QuestList.instance.questList[i]._questName == _NPC_Quest.QuestDetail._questName)
            //{
            //    return true;              
            //}
        }  
        return "Waiting";
    }
    public void ShowCutIn()
    {
        Instantiate(_cutIn, _cutInPos);
    }
}
