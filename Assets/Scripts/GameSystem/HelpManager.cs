
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori sprict

public class HelpManager : MonoBehaviour
{
    /// <summary>
    /// 受注中のタスク
    /// </summary>
    public static string HavingHelpTask;
    public enum HelpKind
    {
        AppleCut,
        KanjiHarvest,
        ClockRepair,
        CakeMake,
    }
    public static bool IsClear = false;
    public static List<bool> IsEndBool 
        = new List<bool>
        {   false, 
            false, 
            false, 
            false 
        };

    //お手伝いを更新したタイミングでログを出す
    private string haveTask;
    private void Update()
    {
        if (haveTask != HavingHelpTask)
        {
            Debug.Log("お手伝い受注：" + HavingHelpTask);
        }
        haveTask = HavingHelpTask;
    }
}
