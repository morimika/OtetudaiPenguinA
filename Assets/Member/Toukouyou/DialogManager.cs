using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _dialogueText, _name;
    [TextArea(1, 3)]
    public string[] _dialogueLine;
    [SerializeField] private int _currentDialogueLine;//�F�ڤλ�Ԓ��
    private bool _isScrolling;//�ƥ����Ȥϥ������`�뤷�Ƥ��뤫�ɤ���

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
        if (_dialogueBox.activeInHierarchy)//�����������ܥå����������
        {
            if (Input.GetMouseButtonDown(0))
            {
               if (_isScrolling == false)//�������`�뤬�K��ä���
                {
                    _currentDialogueLine++;//�Τλ�Ԓ��
                    if (_currentDialogueLine <= _dialogueLine.Length - 1)
                    {
                        CheckName();//Ԓ���֤α�ʾ
                        //_dialogueText.text = _dialogueLine[_currentDialogueLine];
                        StartCoroutine("ScrollingText");
                    }
                    else
                    {
                        _dialogueBox.SetActive(false);//��Ԓ���K���Х����������ܥå�����o����
                        _NPC_Quest.delegateQuest();
                    }
               }
                else
                {
                    //�������`�뤷�Ƥ���С����٤���
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
        _currentDialogueLine = 0;//����Ƥ����Ԓ�Ĥ��ʾ����
        CheckName();//Ԓ���֤α�ʾ
        _dialogueBox.SetActive(true); //�����������ܥå�����ʾ
        //_dialogueText.text = _dialogueLine[_currentDialogueLine];
        StartCoroutine("ScrollingText");
    }

    public void CheckName()
    {
        if (_dialogueLine[_currentDialogueLine].StartsWith("Name-"))
        {
            _name.text = _dialogueLine[_currentDialogueLine].Replace("Name-", "");
            _currentDialogueLine++;//��ǰ��ʾ���뤿��λ�Ԓ�Ĥ��Ԥ�
        }
    }

    private IEnumerator ScrollingText()
    {
        _isScrolling = true;//�������`���_ʼ
        _dialogueText.text = null;//һ������ΤȤ����������֤��ʾ���뤿��
        foreach(char letter in _dialogueLine[_currentDialogueLine].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        _isScrolling = false;
    }

    public bool CheckQuestStatus()
    {
        if(_NPC_Quest == null)
        {
            return false;
        }
        for (int i = 0; i < Player_QuestList.instance.questList.Count;i++) 
        {
            if (Player_QuestList.instance.questList[i]._questStatus == QuestDetail.QuestStatus.Completed
                && Player_QuestList.instance.questList[i]._questName == _NPC_Quest.QuestDetail._questName)
            {
                return true;              
            }
        }
        return false;        
    }
}
