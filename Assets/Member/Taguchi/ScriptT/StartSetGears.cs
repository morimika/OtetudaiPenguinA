using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class StartSetGears : MonoBehaviour
{
    //  プレハブの登録
    [SerializeField] private List<GearController> _gireViewPrefabs = new List<GearController>();

    //  正解のギア番号
    private int _answerIndex = -1;
    //  回答番号のリスト（CSVテキストから抽出したもの）
    private int[] _answerLists = new int[3];

    /// <summary>
    /// 問題文を読み込むと同時に _answerLists に正解、不正解、不正解を登録しておく
    /// </summary>
    private void LoadQuestion()
    {
        //  ファイルを読み込んで、正解、不正解、不正解の順に並べる
    }

    /// <summary>
    /// ランダムにギアを配置
    /// </summary>
    private void RandomMakeGire()
    {
        //  0 ~ 2 までの配列を生成
        var baseIndex = Enumerable.Range(0, 3).ToList();
        //  ランダムになった配列を格納するワークを用意する
        List<int> gireIndexes = new List<int>();
        //  0 ~ 2 までの配列が空になるまで繰り返す
        while (baseIndex.Count > 0)
        {
            //  ０から配列の最大数までの乱数を取得する
            var index = Random.Range(0, baseIndex.Count);
            //  乱数が示す配列の中身を新しい配列にセット
            gireIndexes.Add(baseIndex[index]);
            //  取り出した配列の値を元の配列から削除する
            baseIndex.RemoveAt(index);
        }
        //  ギアのランダム配列の先頭が正解ギアなのでこれを保存しておく
        _answerIndex = gireIndexes[0];
        //  ギアと回答の数値をセットで保存しておく
        for (int id = 0; id < 3; id++)
        {
            var gire = Instantiate(_gireViewPrefabs[gireIndexes[id]]);
            //  gire.transfome.localPosition = new Vector3(x,y,z);
            gire.Setup(gireIndexes[id], _answerLists[id], HitCallback);
        }
    }

    private void HitCallback(int gireIndex)
    {
        if (gireIndex == _answerIndex)
            Debug.Log("正解");
        else
            Debug.Log("不正解");
    }
}
