using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using UnityEditor;

//Mori Script

public class CutInSealAnim : MonoBehaviour
{
    [SerializeField,Label("シールアイコン")]
    private Image _sealImage;
    private GameObject _sealImageObj;
    [SerializeField, Label("プレイヤーのシール情報")]
    private ItemList _playerSeals;
    [SerializeField, Label("背景画像")]
    private CanvasGroup _bgCanvasG;

    public static GameObject _pocketButtonObj;
    private GameObject _pocketButtonParent;

    private bool _isSlideFin = false;

    [SerializeField]
    private PlaySceneDatas _playSceneDatas;

    private bool _isFreeMode = false;
    private bool _isFreeModeFin = false;

    [SerializeField]
    private GameObject _freeBookObj;

    [SerializeField]
    private List<GameObject> _freeBookPicList;

    public List<CanvasGroup> _freeBookPicCanList;

    private GameObject _lastObj;

    private bool _isAll = false;

    [SerializeField]
    private GameObject _finalObj;
    private CanvasGroup _finalObjCanG => _finalObj.GetComponent<CanvasGroup>();

    void Start()
    {
        _pocketButtonParent = GameObject.Find("PocketCanvas");
        _pocketButtonObj = _pocketButtonParent.transform.Find("PocketMenuButton").gameObject;
        _sealImageObj = _sealImage.gameObject;
        //選択したフェードを呼び出し
        StartCoroutine(nameof(SlideLine));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isSlideFin)
        {
            StartCoroutine(nameof(SlideLineOut));
        }
        if (Input.GetMouseButtonDown(0) && _isFreeMode && _isFreeModeFin)
        {
            StartCoroutine(nameof(SlideLineOut));
        }
        Debug.Log(_playSceneDatas.TapType);
    }

    #region
    public IEnumerator SlideLine()
    {
        //取得
        RectTransform rectTransform = GetComponent<RectTransform>();
        CanvasGroup canvasGroup = _sealImage.GetComponent<CanvasGroup>();
        RectTransform sealRectTransform = _sealImage.gameObject.GetComponent<RectTransform>();

        PocketButton.IsTatchAbleButton = false;

        //初期位置にセット
        rectTransform.localPosition = new Vector3(1690, 1690, 0);
        //ラインを細くする
        rectTransform.localScale = new Vector3(0.01f, 1f, 1f);
        //取得シールを設定
        _sealImage.sprite = _playerSeals.items[_playerSeals.items.Count - 1].icon;
        //BGフェードイン
        _bgCanvasG.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
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
        _isSlideFin = false;
        _playSceneDatas.TapType = PlaySceneTapType.Pose;
        //取得
        RectTransform rectTransform = GetComponent<RectTransform>();

        //CanvasGroup canvasGroup = _sealImage.GetComponent<CanvasGroup>();
        ////シール画像のフェードアウト
        //canvasGroup.DOFade(0f, 0.3f);

        //待ったのちラインを狭める
        yield return new WaitForSeconds(0.5f);
        rectTransform.DOScaleX(0.01f, 0.5f);
        //スライドアウト
        rectTransform.DOAnchorPos(new Vector3(-1690, -1690, 0), 1f).SetEase(Ease.InOutQuart);
        yield return new WaitForSeconds(0.5f);
        //BGフェードアウト
        _bgCanvasG.DOFade(0f, 0.5f);

        //シール　ポケットへ
        //ポケットへ移動
        _sealImage.rectTransform.DOAnchorPos(new Vector3(_pocketButtonObj.transform.localPosition.x, _pocketButtonObj.transform.localPosition.y, 0), 0.8f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.6f);
        //移動中にポケット出現
        PocketButton.IsHiddenButton = false;
        _pocketButtonObj.GetComponent<Button>().enabled = false;
        //触れる瞬間にちょっと大きくなって
        _pocketButtonObj.transform.DOScale(2.5f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        //戻る　そのときにシールも非表示
        _sealImageObj.SetActive(false);
        _pocketButtonObj.transform.DOScale(2.0f, 0.2f);
        yield return new WaitForSeconds(0.5f);
        //ポケットを非表示
        _pocketButtonObj.GetComponent<Button>().enabled = true;
        PocketButton.IsHiddenButton = true;

        //自由帳処理(自動)
        //自由帳出現
        var obj = Instantiate(_freeBookObj,new Vector2(Camera.main.transform.position.x+16,Camera.main.transform.position.y),Quaternion.identity) as GameObject;
        var objCan = obj.GetComponent<CanvasGroup>();
        objCan.alpha = 1;
        _freeBookPicCanList.Add(objCan);
        //所持シールに基づき絵を生成
        for (int i = 0; i < _playerSeals.items.Count; i++)
        {
            //生成した最新のオブジェクトを記憶
            _lastObj = Instantiate(_freeBookPicList[_playerSeals.items[i].itemId], new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y), Quaternion.identity) as GameObject;
            var _lastObjCan =_lastObj.GetComponent<CanvasGroup>();
            _lastObjCan.alpha = 1;
            _freeBookPicCanList.Add(_lastObjCan);
            if (_playerSeals.items.Count==4)
            {
                _isAll = true;
            }
        }
        _isFreeMode = true;
        //取得、初期設定　透明化巨大化
        var canvasG = _lastObj.GetComponent<CanvasGroup>();
        canvasG.alpha = 0;
        _lastObj.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        //移動
        obj.transform.DOMoveX(Camera.main.transform.position.x, 0.5f);
        yield return new WaitForSeconds(1);
        //指定の物を大きくフェードインして縮小しながら位置へ　指定のものとは？ 0.02-0.008
        canvasG.DOFade(1, 0.3f);
        _lastObj.transform.DOScale(new Vector3(0.008f, 0.008f, 0.008f), 0.5f).SetEase(Ease.InCirc);

        if (false/*IsAll*/)
        {
            Instantiate(_finalObjCanG);
            _finalObjCanG.alpha = 0;
            _finalObj.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            yield return new WaitForSeconds(1);
            _finalObjCanG.DOFade(1, 0.3f);
            _finalObj.transform.DOScale(new Vector3(0.008f, 0.008f, 0.008f), 0.5f).SetEase(Ease.InCirc);
        }

        //少し見せてから自由帳フェードアウト
        yield return new WaitForSeconds(2);
        for(int i =0;i< _freeBookPicCanList.Count;i++)
        {
            _freeBookPicCanList[i].DOFade(0, 0.5f);
        }

        _playSceneDatas.TapType = PlaySceneTapType.Play;
        PocketButton.IsHiddenButton = false;
        PocketButton.IsTatchAbleButton = true;

        yield return new WaitForSeconds(1);
        for (int i=0;i< _freeBookPicCanList.Count;i++)
        {
            Destroy(_freeBookPicCanList[i].gameObject);
        }
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
        StartCoroutine(nameof(SlideLine));
    }
}
