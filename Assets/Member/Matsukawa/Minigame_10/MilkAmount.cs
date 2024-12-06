using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Matsukawa
public class MilkAmount : MonoBehaviour
{
    #region インスペクター上　ミルク量の取得
    // ミルク樽
    [SerializeField, Foldout("ミルク樽")] private Image         _caskImage;
    // ミルク樽　現在の　ミルク量
    [SerializeField, Foldout("ミルク樽")] private float         _caskCarrentAmount = 1.0f;
    // ミルク樽　最大の　ミルク量
    [SerializeField, Foldout("ミルク樽")] private float         _caskMaxAmount      = 1f;

    // ボトル１（左）
    [SerializeField, Foldout("ボトル１(左)")] private Image      _bottleImage;
    // ボトル１（左） 現在の　ミルク量
    [SerializeField, Foldout("ボトル１(左)")] private float      _bottleCarrentAmount = 0f;
    // ボトル１（左） 最大の　ミルク量
    [SerializeField, Foldout("ボトル１(左)")] private float      _bottleMaxAmount     = 0.8f;
    // ボトル１（左） 正解の　ミルク量
    [SerializeField, Foldout("ボトル１(左)")] private float      _bottleCorrectAmount;

    // ボトル２（右）
    [SerializeField, Foldout("ボトル２(右)")] private Image      _bottle2_Image;
    // ボトル２（右） 現在の　ミルク量
    [SerializeField, Foldout("ボトル２(右)")] private float      _bottle2_carrentAmount = 0f;
    // ボトル２（右） 最大の　ミルク量
    [SerializeField, Foldout("ボトル２(右)")] private float      _bottle2_maxAmount     = 0.8f;
    // ボトル２（右） 正解の　ミルク量
    [SerializeField, Foldout("ボトル２(右)")] private float      _bottle2_correctAmount;
    #endregion

    // ボトル１を押しているときのフラグ
    private bool _ClickBottleFlag;
    // ボトル２を押しているときのフラグ
    private bool _ClickBottleFlag2;

    // そそいでいるときに時間を計算する
    private float seconds;


    // Start is called before the first frame update
    void Start()
    {
        // image を gameObject として取得
        _caskImage.GetComponent<Image>().fillAmount     = 1f;
        _bottleImage.GetComponent<Image>().fillAmount   = 0f;
        _bottle2_Image.GetComponent<Image>().fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_ClickBottleFlag && _bottleImage.fillAmount >= 0)
        {
            seconds += Time.deltaTime;
            // caskがbottleのうえでどんどんかたむく
            // _bottleCarrentAmountが増える
            _bottleImage.fillAmount += seconds / 1000;

            // _caskCarrentAmount を一定の速度で減らす
                _caskImage.fillAmount -= seconds / 1000;
        }

        if (_ClickBottleFlag2 && _bottle2_Image.fillAmount >= 0)
        {
            seconds += Time.deltaTime;
            // caskがbottle2のうえでどんどんかたむく
            // _bottle2_carrentAmountが増える
            _bottle2_Image.fillAmount += seconds / 1000;

            // _caskCarrentAmount を一定の速度で減らす
                _caskImage.fillAmount -= seconds / 1000;

        }
    }

    public void OnBottleButtonDown()
    {
        // ボトル１が押されているとき
        _ClickBottleFlag = true;
        Debug.Log("bottle1 押されている");

    }
    public void OnBottleButtonUp()
    {
        _ClickBottleFlag = false;
        Debug.Log("bottle1 離された");
    }

    public void OnBottle2_ButtonDown()
    {
        // ボトル２が押されている
        _ClickBottleFlag2 = true;
        Debug.Log("bottle2 押されている");
    }
    public void OnBottle2_ButtonUp()
    {
        _ClickBottleFlag = false;
        Debug.Log("bottle2 離された");
    }

    // ミルク瓶(_caskCarrentAmount) の 量を減らす
    public void CaskMilkDown(float current, int max)
    {
        _caskImage.GetComponent<Image>().fillAmount = current / max;
    }
}
