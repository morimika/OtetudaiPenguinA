using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StringPackData
{
    //書き順　を決めるクラス
    [Serializable]
    public class StringData
    {
        //座標を保存する場所
        public List<Vector3> StringPacks;
        public StringData(List<Vector3> stringPacks)
        {
            StringPacks = stringPacks;
        }

    }
    //書き順を貯める場所
    public List<StringData> StringDatas = new List<StringData>();
}
