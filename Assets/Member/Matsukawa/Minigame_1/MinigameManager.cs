using DG.Tweening;
using NaughtyAttributes;
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

    [SerializeField, Label("問題文全体")] GameObject _questionImageAll;     // 問題文全体
    [SerializeField, Label("問題文のもくもく")] Image _questiomCircle;  // 問題文のもくもく
    [SerializeField, Label("問題文")] Image _questionImage;      // 問題文
    [SerializeField, Label("問題文の背景")] Image _bgQuestionImage;    // 問題文の背景
    [SerializeField, Label("問題文の問題文を言うぺんぎん背景")] Image _questionPenguin;    // 問題文を言うぺんぎん

    [SerializeField, Label("問題文の移動先")] Vector2 _qusetionImageTargetPos;   // 問題文が移動する先

    void Start()
    {
        // startImageの現在位置を取得
        RectTransform startImageRect = _questionImage.GetComponent<RectTransform>();
        // 
        _questionImageAll.SetActive(true);
        DOVirtual.DelayedCall(3, () => MoveImage(startImageRect, _qusetionImageTargetPos, _bgQuestionImage));
        DOVirtual.DelayedCall(3, () => _questiomCircle.DOFade(0, 1));
        DOVirtual.DelayedCall(3, () => _questionPenguin.DOFade(0, 1));

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
}
