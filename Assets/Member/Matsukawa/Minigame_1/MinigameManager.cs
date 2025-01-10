using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.UI;

// Matsukawa
public class MinigameManager : MonoBehaviour
{
    // 遷移のアニメーション後ゲームが始まる
    // クリアしていたら、動物に話しかけていい
    // クリアできていなければ元居たステージに遷移
    // ゲームのキャッシをクリア
    // クリアしていなければもう一度挑戦することが出来る
    // クリアしていればもう一度挑戦させない

    // ゲームスタート時にゲームスタートイメージを表示
    // ゲーム終了時、失敗したらミスイメージ、成功したらクリアイメージを表示

    [SerializeField] GameObject startQ;
    [SerializeField] Image startImage;
    [SerializeField] Image bg_startImage;
    [SerializeField] Image finishClearImage;
    [SerializeField] Image finishMissImage;

    [SerializeField] Vector2 startImageTargetPos;

    //mori
    [SerializeField]
    private AppleController _appleController;

    bool isMiss = false;
    // bool isClear = false;


    void Start()
    {
        // startImageの現在位置を取得
        RectTransform startImageRect = startImage.GetComponent<RectTransform>();
        // 
        startQ.SetActive(true);
        DOVirtual.DelayedCall(3, () => MoveImage(startImageRect, startImageTargetPos, bg_startImage));
        //mori
        Invoke(nameof(StartGame), 3);
    }

    void Update()
    {
        if(isMiss == true)
        {
            finishMissImage.gameObject.SetActive(true);
            // りんごの数と表示を元に戻す
        }
    }

    void MoveImage(RectTransform currentPos, Vector2 targetPos, Image im)
    {
        // 目的位置(RectTransform)に向かって1.0秒かけて移動させる
        currentPos.DOAnchorPos(targetPos, 1.0f);
        // サイズを０．８倍する
        currentPos.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 1);
        // スタートイメージの背景をフェードアウト
        im.DOFade(0, 1);
    }

    //mori 
    private void StartGame()
    {
        _appleController._isStart=true;
    }
}
