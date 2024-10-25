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
    Player = 0x01 << 0,
    Enemy = 0x01 << 1,
    Ground = 0x01 << 2,
    Panel = 0x01 << 3,
    Options = 0x01 << 4,
    
}
[Flags]
public enum PlaySceneType
{
    MapMove = 0x01 << 0,
    
}

[CreateAssetMenu(fileName = "PlaySceneDatas", menuName = "PlaySceneDatas", order = 1)]
public class PlaySceneDatas : ScriptableObject
{
   public PlaySceneTapType TapType = PlaySceneTapType.Player;
}
