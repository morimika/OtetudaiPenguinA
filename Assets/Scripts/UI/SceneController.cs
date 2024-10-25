using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// 自由帳へ遷移
    /// </summary>
    public void ChangeFreeBook()
    {
        SceneManager.LoadScene(nameof(Scenes.Mori_FreeBook));
    }

    /// <summary>
    /// 絵本へ遷移
    /// </summary>
    public void ChangePictureBook()
    {
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
