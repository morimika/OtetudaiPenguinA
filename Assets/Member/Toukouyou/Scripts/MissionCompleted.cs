using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCompleted : MonoBehaviour
{
    public string _questName;
    public bool _isCompleted;

    private void QuestCompleted()
    {
        for (int i = 0; i < Player_QuestList.instance.questList.Count; i++)
        {
            if (_questName == Player_QuestList.instance.questList[i]._questName && Player_QuestList.instance.questList[i]._questStatus == QuestDetail.QuestStatus.Accepted)
            {
                _isCompleted = true;
                Player_QuestList.instance.questList[i]._questStatus = QuestDetail.QuestStatus.Completed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            QuestCompleted();
        }
    }
}