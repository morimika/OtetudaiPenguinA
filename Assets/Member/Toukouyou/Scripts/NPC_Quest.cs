using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Quest : MonoBehaviour
{
    public QuestDetail QuestDetail;


    public void delegateQuest()
    {
        if (QuestDetail._questStatus == QuestDetail.QuestStatus.Waiting)
        {
            Player_QuestList.instance.questList.Add(QuestDetail);
            DialogManager.instance.ShowCutIn();
            QuestDetail._questStatus = QuestDetail.QuestStatus.Accepted;
        }

    }
}