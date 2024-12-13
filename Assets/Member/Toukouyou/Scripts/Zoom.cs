using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public static Zoom instance;
    [SerializeField] private Camera _camera;
    public GameObject _NPC;
    [SerializeField] private GameObject _player;
    private int testNum;
    public bool _isDone;
    public bool _isZoomOut;

    public Player _move;//!!!
                      //                                                   change this to the player movement controll script later
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
    }


    public IEnumerator ZoomIn()
    {
        _move.enabled = false;//disable player movement
        yield return new WaitForSeconds(0.1f);
        var pos = 
            new Vector3((_NPC.transform.position.x + _player.transform.position.x) / 2,
            (_NPC.transform.position.y + _player.transform.position.y) / 2,-10);

        _camera.transform.DOMove(pos, 1f).OnComplete(() => testNum = 1); 

        yield return new WaitUntil(()=> testNum ==1);
        
        while(_camera.orthographicSize>=4)
        {
            _camera.orthographicSize -= Time.deltaTime;
            yield return _camera.orthographicSize == 4.0f;
        }
        _isDone = true;
        DialogManager.instance._testClick = true;
    }
    public IEnumerator ZoomOut()
    {
        while (_camera.orthographicSize <= 5)
        {
            _camera.orthographicSize += Time.deltaTime;
            yield return _camera.orthographicSize == 5.0f;
        }

        var pos =new Vector3(_player.transform.position.x,_player.transform.position.y, -10);
        
        _camera.transform.DOMove(pos, 1f).OnComplete(() => testNum = -1);

        yield return new WaitUntil(() => testNum == -1);

        _isZoomOut = true;
        yield return new WaitForSeconds(0.1f);
        _move.enabled = true;
    }
}
