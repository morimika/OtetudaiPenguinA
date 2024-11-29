using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance is null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance is null)
                {
                    SetupInstance();
                }
            }
            return instance;
        }
    }
    
    /// <summary>
    /// 生成直後に同じものがないかの確認
    /// </summary>
    public virtual void Awake()
    {
        RemoveDuplicates();
    }
    
    /// <summary>
    /// 実体の生成
    /// </summary>
    private static void SetupInstance()
    {
        //  本体が無いか探す
        instance = (T)FindObjectOfType(typeof(T));
        //  無いのであれば生成する
        if (instance is null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            instance     = gameObj.AddComponent<T>();
            DontDestroyOnLoad(gameObj);
        }
    }
    
    /// <summary>
    /// 同じものを再登録しない
    /// </summary>
    private void RemoveDuplicates()
    {
        //  存在しないのならばこのオブジェクトを唯一とし破棄できないようにする
        if (instance is null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        //  すでに存在していればこのオブジェクトは不要なので削除する
        else
        {
            Destroy(gameObject);
        }
    }
}
