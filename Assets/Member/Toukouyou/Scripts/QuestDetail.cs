using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public class QuestDetail 
{
   public enum QuestStatus
    {
        Waiting,
        Accepted,
        Completed,
    }

    public string _questName;
    public QuestStatus _questStatus;
}
