using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori sprict

public class HelpManager : MonoBehaviour
{
    public static string HavingHelpTask;
    public enum HelpKind
    {
        AppleCut,
        WaterCar,
        KanjiHarvest,
    }
    public bool IsClear = false;
}
