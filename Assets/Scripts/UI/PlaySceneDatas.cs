using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーンにおけるタップ情報を区別するためのフラグ管理
/// </summary>
[Flags]
public enum PlaySceneTapType
{
    Play = 0x01 << 0,
    Talk = 0x01 << 1,
    Pose = 0x01 << 2,
    
}
[Flags]
public enum PlaySceneType
{
    MapMove = 0x01 << 0,
    
}

[CreateAssetMenu(fileName = "PlaySceneDatas", menuName = "PlaySceneDatas", order = 1)]
public class PlaySceneDatas : ScriptableObject
{
   public PlaySceneTapType TapType = PlaySceneTapType.Play;
}
