using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class StrokePackData
{
    //  ‰ßŠî€‚ğŠi”[‚·‚éƒNƒ‰ƒX
    [Serializable]
    public class StringData
    {
        //  À•W‚ğ•Û‘¶‚·‚éêŠ
        public List<Transform> StrokePacks;

        public StringData(List<Transform> strokePacks)
        {
            StrokePacks = strokePacks;
        }
    }
    //  ‰ßŠî€‚ğ’™‚ß‚éêŠ
    [FormerlySerializedAs("StrokeDatas")]
    public List<StringData> StrokeDatas = new List<StringData>();
}
