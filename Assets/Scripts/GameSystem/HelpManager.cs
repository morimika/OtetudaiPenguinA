using Live2D.Cubism.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori sprict

public class HelpManager : MonoBehaviour
{
    /// <summary>
    /// �󒍒��̃^�X�N
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
    //����`�����X�V�����^�C�~���O�Ń��O���o��
    private string haveTask;
    private void Update()
    {
        //Debug.Log(IsClear);
        if (haveTask != HavingHelpTask)
        {
            Debug.Log("����`���󒍁F"+HavingHelpTask);
        }
        haveTask = HavingHelpTask;
    }
#endif
}
