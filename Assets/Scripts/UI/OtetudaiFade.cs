using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class OtetudaiFade : MonoBehaviour
{
    private Vector3 _playerPos;
    private GameObject _player;

    private bool _isFade = false;
    private bool _onMouse = false;

    [SerializeField]
    private GameObject _action;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    async Task Update()
    {
        _playerPos = _player.transform.position;
        if (Vector3.Distance(this.transform.position,_playerPos)<=3)
        {
            _action.SetActive(true);
            if (Input.GetMouseButtonDown(0) && _onMouse)
            {
                await ChangeMinigameAsync();
                PlayerSetPos.PlayerPos = _playerPos;
                SceneManager.LoadScene("WatanabeTestScene");
            }
        }
        else
        {
            _action.SetActive(false);
        }
    }

    private async UniTask ChangeMinigameAsync()
    {
        //fade終わったら_isFade=true;
        //フェード処理が終わるまでまつ
        //await UniTask.WaitUntil(() => _isFade == true);
        await UniTask.Delay(1000);
    }

    private void OnMouseDown()
    {
        _onMouse = true;
    }

    private void OnMouseUp()
    {
        _onMouse = false;
    }
}
