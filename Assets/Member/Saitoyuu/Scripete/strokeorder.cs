using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strokeorder : MonoBehaviour
{
   [SerializeField]private List<GameObject> _strokeorderposition = new List<GameObject>();
    private int _number = 0;
    private int _positionNumber = 0;


    public void order()
    {
        conversion conversion = GetComponent<conversion>();
        //_number = conversion._count;
        if(_number == _strokeorderposition.Count) 
        {
            
        }
    }

}
