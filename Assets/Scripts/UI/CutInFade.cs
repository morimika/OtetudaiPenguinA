using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.UIElements;

//Mori Script

public class CutInFade : MonoBehaviour
{
    //プルダウンでインスペクターからフェード形式を選択
    [Dropdown("fadeKinds")]
    public string fadeKind;
    public static List<string> fadeKinds => new List<string>() { nameof(Slide), nameof(Line) };

    public static bool IsFadeFin = false;

    [SerializeField]
    private PlaySceneDatas _playSceneDatas;


    [SerializeField]
    private bool _isClear = false;

    [SerializeField]
    private bool _IsStop = false;

    void Start()
    {
        //選択したフェードを呼び出し
        StartCoroutine(fadeKind);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&IsFadeFin &&_IsStop==false)
        {
            StartCoroutine(fadeKind+"Out");
        }
    }

    #region Slide
    public IEnumerator Slide()
    {
        //取得
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        GameObject penguin = GameObject.Find("Penguin");
        CanvasGroup cancasPenguinGroup = penguin.GetComponent<CanvasGroup>();
        RectTransform rectTransform = GetComponent<RectTransform>();

        //初期位置にセット
        rectTransform.localPosition = new Vector3(3470, 0,0);
        //ラインを透明にする
        canvasGroup.alpha = 0;
        //ラインの大きさを等倍にする
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        //ペンギンを可視化
        cancasPenguinGroup.alpha = 1;
        //スライドイン
        rectTransform.DOAnchorPosX(0, 1f).SetEase(Ease.InOutQuart);
        //フェードイン
        canvasGroup.DOFade(1, 1f);

        yield return new WaitForSeconds(1.2f);
        IsFadeFin =true;

        yield return null;
    }
    public IEnumerator SlideOut()
    {
        //取得
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        RectTransform rectTransform = GetComponent<RectTransform>();

        //フェードアウト
        canvasGroup.DOFade(0, 1f);
        //スライドアウト
        rectTransform.DOAnchorPosX(-3470, 1f).SetEase(Ease.InOutQuart);
        IsFadeFin = false;

        yield return new WaitForSeconds(1);
        if(!_isClear)
        {
            _playSceneDatas.TapType = PlaySceneTapType.Play;
        }
        yield return new WaitForSeconds(0.5f);
        DestroyThis();
    }
    #endregion

    #region Line
    public IEnumerator Line()
    {
        //取得
        GameObject penguin = GameObject.Find("Penguin");
        CanvasGroup canvasGroup = penguin.GetComponent<CanvasGroup>();
        RectTransform rectTransform = GetComponent<RectTransform>();


        //初期位置にセット
        rectTransform.localPosition = new Vector3(3470, 0, 0);
        //ペンギンを透明化
        canvasGroup.alpha = 0;
        //ラインを細くする
        rectTransform.localScale = new Vector3(1f, 0.025f, 1f);
        //スライドイン
        rectTransform.DOAnchorPosX(0, 1f).SetEase(Ease.InOutQuart);
        //待ったのちラインを広げる
        yield return new WaitForSeconds(1f);
        rectTransform.DOScaleY(1, 0.5f);
        //待ったのちペンギンをフェードイン
        yield return new WaitForSeconds(0.5f);
        canvasGroup.DOFade(1, 0.7f);

        yield return new WaitForSeconds(0.3f);
        IsFadeFin = true;
    }

    public IEnumerator LineOut()
    {
        //取得
        GameObject penguin = GameObject.Find("Penguin");
        CanvasGroup canvasGroup = penguin.GetComponent<CanvasGroup>();
        RectTransform rectTransform = GetComponent<RectTransform>();

        //ペンギンをフェードアウト
        canvasGroup.DOFade(0, 0.3f);
        //待ったのちラインを狭める
        yield return new WaitForSeconds(0.5f);
        rectTransform.DOScaleY(0.025f, 0.5f);
        //スライドアウト
        rectTransform.DOAnchorPosX(-3470, 1f).SetEase(Ease.InOutQuart);
        IsFadeFin = false;
        yield return new WaitForSeconds(1);
        if (!_isClear)
        {
            _playSceneDatas.TapType = PlaySceneTapType.Play;
        }
        yield return new WaitForSeconds(0.5f);
        DestroyThis();
    }
    #endregion

    /// <summary>
    /// フェード後消去
    /// </summary>
    private void DestroyThis()
    {
        var parent = this.transform.parent;
        Destroy(parent.gameObject);
    }

    //debug
    [SerializeField, Button]
    private void DebugFade()
    {
        //選択したフェードを呼び出し
        StartCoroutine(fadeKind);
    }
}
