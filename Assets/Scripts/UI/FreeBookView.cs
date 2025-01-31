using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBookView : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _freeBookPicList;

    void Start()
    {
        FreeBookImageView();
    }

    void Update()
    {
        
    }

    public void FreeBookImageView()
    {
        for (int i = 0; i < _freeBookPicList.Count; i++)
        {
            if (HelpManager.IsEndBool[i] == true)
            {
                _freeBookPicList[i].SetActive(true);
            }
        }
    }
    
}
