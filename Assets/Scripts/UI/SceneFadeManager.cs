using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneFadeManager : MonoBehaviour
{
    /*疑問フェードインが終わったってどうやって判定するのか？
     *Unitaskでやるのか？
     * 一旦　アニメーション内で変数呼び出すことにする
     * フェードが終わったらキャンバスを非アクティブにする
     * シーンを呼び出す時は終わるまでポーズモードにする
     * アニメーションを再生する
     * シーン先でアクティブ状態なら、アニメーションを再生
     * その後にプレイモードに変更してタップできるようにする
     */
    
    

    [SerializeField, Header("遷移前か遷移後かの判定")]
    private bool _isPreScene;

    [SerializeField, Header("フェードアニメーションキャンバス")]
    private Canvas _fadeAnimationCanvas;
    
    [SerializeField,Header("ゲームモードデータ")]
    private PlaySceneDatas _playSceneDatas;

    public static bool _isTitle;
    
    
    // Start is called before the first frame update
    void Start()
    {
       
        //遷移前なのか遷移後なのかの判定
        if (_isPreScene)
        {
            //キャンバスがアクティブかどうか判定する
            CanvasActiveCheck();
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //タイトル画面かどうかの判定
        if (_isTitle) TitleAnimation();

    }

    /// <summary>
    /// タイトルの時に呼び出される処理
    /// </summary>
    public void TitleAnimation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //イメージやボタンなどをクリックしても何も反応しないようにする
            if (EventSystem.current.IsPointerOverGameObject()) return;
            else
            {
                _isTitle = false;
                //キャンバスがアクティブかどうかをチェック
                CanvasActiveCheck();
                //シーンをロード
                //SceneManager.LoadScene("Yuria_PlayScene");

            }
            
        }
    }
    /// <summary>
    /// キャンバスがアクティブかどうかのチェック
    /// </summary>
    public void CanvasActiveCheck()
    {
        //キャンバスがアクティブでないならアクティブにする
        if (_fadeAnimationCanvas == false)
        {
            _fadeAnimationCanvas.gameObject.SetActive(true);
        }
        else
        {
            _fadeAnimationCanvas.gameObject.SetActive(false);
        }
    }
}
