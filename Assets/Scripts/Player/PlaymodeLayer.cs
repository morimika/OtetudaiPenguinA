using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaymodeLayer : MonoBehaviour
{
    private SpriteRenderer _spriteLayer;
    private Camera _camera;
    private float sprBounds = 0;

    private void Start()
    {
        _spriteLayer = GetComponent<SpriteRenderer>();
        _camera=Camera.main;
        sprBounds = _spriteLayer.bounds.extents.y;
    }
    
    private void OnWillRenderObject()
    {
        Vector3 vec
            = _camera.WorldToScreenPoint(new Vector3(0,this.transform.position.y - sprBounds, 0));
        _spriteLayer.sortingOrder = ((int)vec.y*-1)-500;
    }

}
