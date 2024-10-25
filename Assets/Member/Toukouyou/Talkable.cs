using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool _isTalkable;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _questionText;
    [TextArea(1, 3)]
    public string[] _line;
    private DialogManager _dialogManager;
    [SerializeField] private RectTransform _rectTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _isTalkable = true;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);//ダイアログボックスのサイズ調整
            _questionText.text = "困ったな";//テキスト調整
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTalkable = false;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);//ダイアログボックスのサイズ調整
            _questionText.text = "?";//テキスト調整
        }
    }

    private void Update()
    {
        //PlayerがNPC範囲内、会話始まっていない、画面をタップ
        if (_isTalkable && _dialogueBox.activeInHierarchy == false && Input.GetKeyDown(KeyCode.Space))
        {            
            FindObjectOfType<DialogManager>().ShowDialogue(_line);//ダイアログボックス表示          
        }
    }
}
