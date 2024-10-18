using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _dialogueText, _name;
    [TextArea(1, 3)]
    public string[] _dialogueLine;
    [SerializeField] private int _currentDialogueLine;//現在の会話文
    private bool _isScrolling;//テキストはスクロールしているかどうか

    //void Start()
    //{
    //    _dialogueText.text = _dialogueLine[_currentDialogueLine];
    //}

    void Update()
    {
        if (_dialogueBox.activeInHierarchy)//ダイアログボックスがあれば
        {
            if (Input.GetMouseButtonDown(0))
            {
               if (_isScrolling == false)//スクロールが終わったら
                {
                    _currentDialogueLine++;//次の会話文
                    if (_currentDialogueLine <= _dialogueLine.Length - 1)
                    {
                        CheckName();//話し手の表示
                        //_dialogueText.text = _dialogueLine[_currentDialogueLine];
                        StartCoroutine("ScrollingText");
                    }
                    else
                    {
                        _dialogueBox.SetActive(false);//会話が終わればダイアログボックスを無くす
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
        CheckName();//話し手の表示
        _dialogueBox.SetActive(true); //ダイアログボックス表示
        //_dialogueText.text = _dialogueLine[_currentDialogueLine];
        StartCoroutine("ScrollingText");
    }

    public void CheckName()
    {
        if (_dialogueLine[_currentDialogueLine].StartsWith("Name-"))
        {
            _name.text = _dialogueLine[_currentDialogueLine].Replace("Name-", "");
            _currentDialogueLine++;//名前表示するための会話文を略す
        }
    }

    private IEnumerator ScrollingText()
    {
        _isScrolling = true;//スクロール開始
        _dialogueText.text = null;//一番最初のところから文字を表示するため
        foreach(char letter in _dialogueLine[_currentDialogueLine].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        _isScrolling = false;
    }
}
