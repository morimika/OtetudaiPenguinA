using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetDebug : MonoBehaviour
{
    [SerializeField,ReadOnly]
    private List<HelpInfo> _helpInfo;

    [SerializeField]
    private ItemList ItemList;

    private void Start()
    {
        GameObject[] npcObj = GameObject.FindGameObjectsWithTag("NPC");
        for (int i = 0; i < npcObj.Length; i++)
        {
            _helpInfo.Add(npcObj[i].GetComponent<HelpInfo>());
        }
    }

    public void ResetGame()
    {
        //お手伝い終了状況のリセット
        for (int i = 0; i < HelpManager.IsEndBool.Count; i++)
        {
            HelpManager.IsEndBool[i] = false;
        }
        //お手伝い受注状況のリセット
        HelpManager.HavingHelpTask = null;
        HelpManager.IsClear = false;
        PocketButton.IsHiddenButton = false;

        //for (int i = 0; i < HelpManager.IsClear.Count; i++)
        //{
        //    HelpManager.IsClear[i] = false;
        //}

        //シール獲得状況のリセット
        ItemList.items.Clear();

        //プレイヤーの保存座標のリセット
        PlayerSetPos.PlayerPos = Vector2.zero;
        //タイトルへ戻る
        SceneManager.LoadScene("Yuria_TitleScene");
    }
}
