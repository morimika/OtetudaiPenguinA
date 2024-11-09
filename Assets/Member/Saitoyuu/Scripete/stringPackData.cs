using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class stringPackData
{
    //書き順　を決めるクラス
    [Serializable]
    public class strokcData
    {
        //座標を保存する場所
        public List<Vector3> StringPacks;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           

        public strokcData(List<Vector3> StringPacks)
        {
            StringPacks = StringPacks;
        }

    }
    //書き順を貯める場所
    public List<strokcData> StrokcDatas = new List<strokcData>();
}
