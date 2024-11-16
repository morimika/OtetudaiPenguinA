using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シングルトンはたった一つしかないもののこと　以下は作法
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    //対応するクラスのinstanceを保持する場所
    private static T _instance;
    
    //クラスを作成
    public static T Instance
    {
        get
        {
            //instanceがあるかどうか
            if (_instance is null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance is null)
                {
                    //新規生成する
                }
            }
            //見つかった本体を返す
            return _instance;
        }
    }

    /// <summary>
    /// クラスの実体（Only one）を生成
    /// </summary>
    private static void SetUpInstance()
    {
        //新規にゲームオブジェクトを生成する
        GameObject go = new GameObject();
        //それに名前をつける
        go.name = typeof(T).Name;
        //実体を生成する
        _instance = go.AddComponent<T>();
        //シーンが変わっても存在できるようにする
        DontDestroyOnLoad(go);
    }

    /// <summary>
    /// 起動時にチェックする
    /// </summary>
    public virtual void Awake()
    {
        RemoveDuplicates();
    }
    
/// <summary>
/// 同じものを再生成しようとしたら、そのオブジェクトは破棄
/// </summary>
    private void RemoveDuplicates()
    {
        //存在しないなら、生成
        if (_instance is null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        //存在するなら破棄する
        else
        {
            Destroy(gameObject);
        }
    }
}
