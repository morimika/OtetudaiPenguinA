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

    [TextArea(1, 3)]
    public string[] _questCompletedLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _isTalkable = true;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);//ダイアログボックスのサイズ調整
            _questionText.text = "help";//テキスト調整
            DialogManager.instance._NPC_Quest = GetComponent<NPC_Quest>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTalkable = false;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);//ダイアログボックスのサイズ調整
            _questionText.text = "?";//テキスト調整
            DialogManager.instance._NPC_Quest = null;
        }
    }

    private void Update()
    {
        //PlayerがNPC範囲内、会話始まっていない、画面をタップ
        if (_isTalkable && _dialogueBox.activeInHierarchy == false && Input.GetKeyDown(KeyCode.Space))
        {
            if (DialogManager.instance.CheckQuestStatus() == true)
            {
                _line = _questCompletedLine;
            }
                DialogManager.instance.ShowDialogue(_line);//ダイアログボックス表示                    
        }
    }
}
