using NaughtyAttributes;
using System.IO;
using System.Linq;
using UnityEngine;


public class MaikeStringData : MonoBehaviour
{
    private int count = 0;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _positionPrefab;

    #region ヒエラルキーのデータを保存する場所
    [SerializeField]
    private StrokePackData _strokePackData = new StrokePackData();
    #endregion

    #region データー保存処理
    [Button]
    private void MakeStrokeData()
    {
        CommonParam.StringPackDatas.StringDatas.Clear();
        foreach(var index in Enumerable.Range(0,_strokePackData.StrokeDatas.Count))
        {
            var strokeData =
                _strokePackData.StrokeDatas[index]
                                .StrokePacks
                                .Select(strData => strData.transform.position)
                                .ToList();
            CommonParam.StringPackDatas.StringDatas.Add(new StringPackData.StringData(strokeData));
        }
        var         path = $"{Application.dataPath}/{CommonParam.SaveFileName}.json";
        using var   stw = new StreamWriter(path,false);
        Debug.Log(CommonParam.StringPackDatas.StringDatas[0].StringPacks.Count);
        var json = JsonUtility.ToJson(CommonParam.StringPackDatas);
        Debug.Log(json);
        stw.Write(json);
    }
    #endregion

    
    //private stringPackData _stringPackDate;
    //private string saveFileName = "MinGameStrokeorder.json";



    #region データの読み込み
    [Button]
    private void LoadStrokData()
    {
        var          path = $"{Application.dataPath}/{CommonParam.SaveFileName}.json";
        using var    str  = new StreamReader (path);
        CommonParam.StringPackDatas = JsonUtility.FromJson<StringPackData>(str.ReadToEnd());  

        _strokePackData.StrokeDatas.Clear();

        foreach (var index in Enumerable.Range(0,CommonParam.StringPackDatas.StringDatas.Count))
        {
            //レベルオブジェクトの生成
            var levelObject = new GameObject($"{(index + 1)}");
            levelObject.transform.SetParent(_parent);
            
            var strokeData =
                CommonParam.StringPackDatas.StringDatas[index].StringPacks.Select(strData =>
                {
                    var posObject = Instantiate(_positionPrefab, levelObject.transform);
                    posObject.name = count.ToString();
                    count++;
                    posObject.transform.position = strData;
                    return posObject.transform;
                }).ToList();
            _strokePackData.StrokeDatas.Add(new StrokePackData.StringData(strokeData));


        }
        

    }
    #endregion
}