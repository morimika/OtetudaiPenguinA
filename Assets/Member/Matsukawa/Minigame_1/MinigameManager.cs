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

    [SerializeField] Image startImage;
    [SerializeField] Image finishClearImage;
    [SerializeField] Image finishMissImage;

    [SerializeField] Vector2 startImageTargetPos;

    bool isMiss = false;
    // bool isClear = false;


    void Start()
    {
        // startImageの現在位置を取得
        RectTransform startImageRect = startImage.GetComponent<RectTransform>();
        // スタートイメージが表示されてから秒後に左上に移動
        startImage.gameObject.SetActive(true);
        DOVirtual.DelayedCall(1, () => MoveImage(startImageRect, startImageTargetPos));
    }

    void Update()
    {
        if(isMiss == true)
        {
            finishMissImage.gameObject.SetActive(true);
            // りんごの数と表示を元に戻す
        }
    }

    void MoveImage(RectTransform currentPos, Vector2 targetPos)
    {
        // 目的位置(RectTransform)に向かって0.8秒かけて移動させる
        currentPos.DOAnchorPos(targetPos, 0.5f);
    }
}
