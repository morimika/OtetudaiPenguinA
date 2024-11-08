using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

//Mori Script

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
        //プレイヤーのポジションを更新
        _playerPos = _player.transform.position;
        //距離を測って物と近いか確認
        if (Vector3.Distance(this.transform.position,_playerPos)<=3)
        {
            //押せるUI出現
            _action.SetActive(true);
            //物をクリックしたとき
            if (Input.GetMouseButtonDown(0) && _onMouse)
            {
                //フェードを待ち、遷移する
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

    /// <summary>
    /// 押しているか
    /// </summary>
    private void OnMouseDown()
    {
        _onMouse = true;
    }

    private void OnMouseUp()
    {
        _onMouse = false;
    }
}
