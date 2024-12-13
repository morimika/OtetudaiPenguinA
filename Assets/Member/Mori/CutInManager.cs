using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori Script

public class CutInManager : MonoBehaviour
{
    public GameObject StartCutInPre;
    public GameObject ClearCutInPre;
    public GameObject SealCutInPre;

    public static bool Fin = false;

    public void StartCutIn()
    {
        Instantiate(StartCutInPre);
    }
    public void ClearCutIn()
    {
        Instantiate(ClearCutInPre);
    }
    public void SealCutIn()
    {
        Instantiate(SealCutInPre);
    }
}
