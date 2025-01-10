using Live2D.Cubism.Core;
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
        WaterCar,
        KanjiHarvest,
    }
    public static bool IsClear = false;
    public static List<bool> IsEndBool 
        = new List<bool>
        {   false, 
            false, 
            false 
        };

#if UNITY_EDITOR
    //お手伝いを更新したタイミングでログを出す
    private string haveTask;
    private void Update()
    {
        Debug.Log(IsClear);
        if (haveTask != HavingHelpTask)
        {
            Debug.Log("お手伝い受注："+HavingHelpTask);
        }
        haveTask = HavingHelpTask;
    }
#endif
}
