using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RepeatQuestChoice : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private Canvas _maskBlock;
    private CanvasGroup _canvasGroup;
    [SerializeField] private Button _YESButton;
    [SerializeField] private Button _NOButton;
    public move _move;//!!!
                      //                                     change this to the player movement controll script later
    void Start()
    {
        _maskBlock.enabled = false;
        _canvas = GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvas.enabled = false;
        _YESButton.onClick.AddListener(() => { ChooseYes(); });
        _NOButton.onClick.AddListener(() => { ChooseNo(); });
    }

    private void ChooseYes()
    {
        _maskBlock.enabled = false;
        _canvas.enabled = false;
        _canvasGroup.alpha = 0;
        _move.enabled = true;
        DialogManager.instance._NPC_Quest.QuestDetail._questStatus = QuestDetail.QuestStatus.Accepted;
        DialogManager.instance.ShowStartCutIn();
    }
    private void ChooseNo()
    {
        _maskBlock.enabled = false;
        _canvas.enabled = false;
        _canvasGroup.alpha = 0;
        _move.enabled = true;
    }

    public void ShowCanvas()
    {
        _maskBlock.enabled = true;
        _canvasGroup.alpha = 1;
        _canvas.enabled = true;
        _move.enabled = false;
    }

}
