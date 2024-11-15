using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StrokePackData
{
    //‘‚«‡‚ğŠi”[‚·‚éƒNƒ‰ƒX
    [Serializable]
    public class StrokeData
    {
        //À•W‚ğ•Û‘¶‚·‚éêŠ
        public List<Transform> StringPacks;

        public StrokeData(List<Transform> StringPacks)
        {
            StringPacks = StringPacks;
        }
    }

    //‘‚«‡‚ğ’™‚ß‚éêŠ
    public List<StrokeData> StrokeDatas = new List<StrokeData>();
}
