using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBookView : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _freeBookPicList;

    void Start()
    {
        for (int i = 0; i < _freeBookPicList.Count; i++)
        {
            if (HelpManager.IsEndBool[i] == true)
            {
                var obj = Instantiate(_freeBookPicList[i],transform);
            }
        }
    }

    void Update()
    {
        
    }
}
