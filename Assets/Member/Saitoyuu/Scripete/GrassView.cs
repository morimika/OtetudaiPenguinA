using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassView : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
     void Start()
    {
           _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetCheckColor()
    {
        _spriteRenderer.color = Color.yellow;
    }

}
