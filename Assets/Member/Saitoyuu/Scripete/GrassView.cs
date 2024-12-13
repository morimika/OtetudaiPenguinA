using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassView : MonoBehaviour
{
    public Sprite _newSprite;
    private SpriteRenderer _spriteRenderer;
     void Start()
    {
           _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetCheckColor()
    {
        _spriteRenderer.sprite = _newSprite;
        _spriteRenderer.color = Color.yellow;
    }

}
