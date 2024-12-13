using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Quest : MonoBehaviour
{
    public QuestDetail QuestDetail;
    public void delegateQuest()
    {
        //delegate a quest only in the status of Waiting
        if (QuestDetail._questStatus == QuestDetail.QuestStatus.Waiting)
        {
            Player_QuestList.instance.questList.Add(QuestDetail);
            DialogManager.instance.ShowStartCutIn();
            QuestDetail._questStatus = QuestDetail.QuestStatus.Accepted;//change the status after delegate the quest
        }
    }
}