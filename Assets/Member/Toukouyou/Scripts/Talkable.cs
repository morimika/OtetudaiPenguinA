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
    private DialogManager _dialogManager;
    [SerializeField] private RectTransform _dialogueBox_rectTransform;
    [SerializeField] private RectTransform _text_rectTransform;

    [TextArea(1, 3)]
    public string[] _questCompletedLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _isTalkable = true;
            _dialogueBox_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);//ダイアログボックスのサイズ調整
            _text_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
            _questionText.text = "困ったな...";//テキスト調整
            DialogManager.instance._NPC_Quest = GetComponent<NPC_Quest>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTalkable = false;
            _dialogueBox_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);//ダイアログボックスのサイズ調整
            _text_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
            _questionText.text = "?";//テキスト調整
            DialogManager.instance._NPC_Quest = null;
        }
    }

    private void Update()
    {
        //PlayerがNPC範囲内、会話始まっていない、画面をタップ
        if (_isTalkable && _dialogueBox_talking.activeInHierarchy == false && Input.GetKeyDown(KeyCode.Space))
        {
            if (DialogManager.instance.CheckQuestStatus() == "Completed")
            {
                _line = _questCompletedLine;
            }
                DialogManager.instance.ShowDialogue(_line);//ダイアログボックス表示                    
        }
    }
}
