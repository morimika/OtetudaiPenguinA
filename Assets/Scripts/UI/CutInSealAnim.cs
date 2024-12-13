using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

//Mori Script

public class CutInSealAnim : MonoBehaviour
{
    [SerializeField,Label("シールアイコン")]
    private Image _sealImage;
    [SerializeField, Label("プレイヤーのシール情報")]
    private ItemList _playerSeals;

    private bool _isSlideFin = false;

    [SerializeField]
    private PlaySceneDatas _playSceneDatas;

    void Start()
    {
        //選択したフェードを呼び出し
        StartCoroutine(nameof(SlideLine));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isSlideFin)
        {
            StartCoroutine(nameof(SlideLineOut));
        }
    }

    #region
    public IEnumerator SlideLine()
    {
        //取得
        RectTransform rectTransform = GetComponent<RectTransform>();
        CanvasGroup canvasGroup = _sealImage.GetComponent<CanvasGroup>();
        RectTransform sealRectTransform = _sealImage.gameObject.GetComponent<RectTransform>();

        Debug.Log(_sealImage);

        //初期位置にセット
        rectTransform.localPosition = new Vector3(1690, 1690, 0);
        //ラインを細くする
        rectTransform.localScale = new Vector3(0.01f, 1f, 1f);
        //取得シールを設定
        _sealImage.sprite = _playerSeals.items[_playerSeals.items.Count - 1].icon;
        //スライドイン
        rectTransform.DOAnchorPos(new Vector3(0,0,0), 1f).SetEase(Ease.InOutQuart);
        //待ったのちラインを広げる
        yield return new WaitForSeconds(1f);
        rectTransform.DOScaleX(1, 0.5f);
        //待ったのちシールを表示する
        yield return new WaitForSeconds(0.5f);
        canvasGroup.DOFade(1f, 0.3f);
        //シールを拡大縮小で強調表示したい
        //yield return new WaitForSeconds(0.5f);
        //sealRectTransform.DOScale(new Vector3(6, 6, 6), 1f);/*.SetLoops(1, LoopType.Yoyo);*/

        yield return new WaitForSeconds(0.3f);
        _isSlideFin = true;
    }

    public IEnumerator SlideLineOut()
    {
        //取得
        RectTransform rectTransform = GetComponent<RectTransform>();
        CanvasGroup canvasGroup = _sealImage.GetComponent<CanvasGroup>();

        canvasGroup.DOFade(0f, 0.3f);
        //待ったのちラインを狭める
        yield return new WaitForSeconds(0.5f);
        rectTransform.DOScaleX(0.01f, 0.5f);
        //スライドアウト
        rectTransform.DOAnchorPos(new Vector3(-1690, -1690, 0), 1f).SetEase(Ease.InOutQuart);
        _isSlideFin = false;

        yield return new WaitForSeconds(1);
        _playSceneDatas.TapType = PlaySceneTapType.Play;
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
        StartCoroutine(nameof(SlideLine));
    }
}
