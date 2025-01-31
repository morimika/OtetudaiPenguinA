using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClereUIMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOMove(new Vector3(900, 550, 0f), 1f);
    }
}
