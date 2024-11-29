using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mori Script

public class CutInManager_Mori : MonoBehaviour
{
    [SerializeField]
    private GameObject _cutInCanvas;

    [SerializeField]
    private GameObject _sealGetCanvas;

    [SerializeField,Button]
    private void CutIn()
    {
        Instantiate(_cutInCanvas);
    }

    [SerializeField, Button]
    private void SealCutIn()
    {
        Instantiate(_sealGetCanvas);
    }
}
