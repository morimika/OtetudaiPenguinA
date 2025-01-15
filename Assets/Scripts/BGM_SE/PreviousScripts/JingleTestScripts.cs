using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class JingleTestScripts : MonoBehaviour
{
    
    /*
     * 以下はジングルを鳴らすためだけのデバックスクリプト削除予定
     * 菱沼
     */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
          TestPlayJingleOBJ(other.gameObject).Forget();
        }
            
    }

    private async UniTask TestPlayJingleOBJ(GameObject gameObject)
    {
        // ジングルを非同期で再生
        await BSJSoundManger.Instance.PlayJingleAsync(0);

        // ジングル終了後の処理を記述
        Debug.Log("ジングル再生が終了しました！");
        
    }
}
