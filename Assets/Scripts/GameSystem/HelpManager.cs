using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//Mori Script

/// <summary>
/// お手伝い管理
/// HelpManager.helpNameEnum = HelpManager.HelpNameEnum.AppleGet;
/// HelpNameを変更する事で受注可能
/// HelpInfo.isClear=true
/// でお手伝いクリアのフラグが立つ
/// </summary>
public class HelpManager : HelpInfo
{
    public enum HelpNameEnum
    {
        None,
        AppleGet,
        WaterCar,
        KanjiHarvest,
    }

    [SerializeField]
    public static HelpNameEnum helpNameEnum;

    void Start()
    {

    }

    void Update()
    {
        helpName = helpNameEnum.ToString();

        if(helpName==HelpNameEnum.None.ToString())
        {
            Debug.Log("NONjyuytu");
        }
        else
        {
            Debug.Log("jyuytu");
        }
    }
}

public class HelpInfo:MonoBehaviour
{
    public string helpName;
    public static bool isOrder = false;
    public static bool isClear = false;
}