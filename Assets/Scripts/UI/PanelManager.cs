
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    //ポーズ中かどうかの判定
    public static bool _isPaused;
    [SerializeField,Header("ゲームモードデータ")] private PlaySceneDatas _playSceneDatas;
    private Button _resumeButton;

    [SerializeField] private PlayerTapMove _playerTapMove;
    //表示するパネル先
    [SerializeField] private GameObject _pauseMenuUI;

    private bool _isTap = false;
    private float _tapTimer = 0f;
    private readonly float TapWaitMax = 1f;

    void Start()
    {
        //戻るボタン取得
        _resumeButton = GetComponent<Button>();
    }

    void Update()
    {
        if (_isTap)
        {
            _tapTimer += Time.deltaTime;
            if (_tapTimer >= TapWaitMax)
            {
                _isTap = false;
                _tapTimer = 0;
            }
        }
    }

    public void OnTap()
    {
        // if (_isTap) return;
        // _isTap = true;
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
            
        }
    }

    /// <summary>
    /// ゲームに戻る処理
    /// </summary>
    public void Resume()
    {
        //パネルをアクティブ
        // gameObject.SetActive(false);
        //プレイヤーモードに変更する
        _playSceneDatas.TapType = PlaySceneTapType.Player;
        //パネルを消す
        _pauseMenuUI.SetActive(false);
        _isPaused = false;

    }
    


    public void Pause()
    {
        _isPaused = true;
        //すべてのフラグを初期化
        _playerTapMove.ClearAllFlags();
         //パネルに変更する
        _playSceneDatas.TapType = PlaySceneTapType.Panel;
        //パネルの表示
        _pauseMenuUI.SetActive(true);
        
        
    }

    /// <summary>
    /// 戻るボタンを押した時の処理
    /// </summary>
    public void ClosePanel()
    {
        //モードをプレイヤーに戻す
        _playSceneDatas.TapType = PlaySceneTapType.Player;
        //パネルを消す
        _pauseMenuUI.SetActive(false);
        //コルーチンをスタートさせる
        StartCoroutine(WaitFrame());
    }

    //1秒待つ
    private IEnumerator WaitFrame()
    {
        yield return new WaitForSeconds(1);
        _isPaused = false;
        
    }
}
