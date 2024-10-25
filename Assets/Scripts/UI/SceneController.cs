using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

//Mori Script

/// <summary>
/// シーン遷移に使える変数を入れるスクリプト
/// </summary>
public class SceneController : MonoBehaviour
{
    //シーン名のリスト
    private enum Scenes
    {
        Yuria_TitleScene,
        Yuria_PlayScene,
        Mori_MainGameScene,
        Mori_FreeBook,
        Mori_PictureBook
    }

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// 自由帳へ遷移
    /// </summary>
    public void ChangeFreeBook()
    {
        //プレイヤーの位置情報を保存する
        PlayerSetPos.PlayerPos = _player.transform.position;
        SceneManager.LoadScene(nameof(Scenes.Mori_FreeBook));
    }

    /// <summary>
    /// 絵本へ遷移
    /// </summary>
    public void ChangePictureBook()
    {
        //プレイヤーの位置情報を保存する
        PlayerSetPos.PlayerPos=_player.transform.position;
        SceneManager.LoadScene(nameof(Scenes.Mori_PictureBook));
    }

    /// <summary>
    /// メインシーンへ遷移
    /// </summary>
    public void ChangeMainScene()
    {
        SceneManager.LoadScene(nameof(Scenes.Mori_MainGameScene));
    }
}
