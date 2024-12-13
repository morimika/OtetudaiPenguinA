using UnityEngine;

public class FadeView : MonoBehaviour
{
    /// アニメーションが終わったら呼び出されるコールバック関数
    private System.Action _onFadeComplete;
    
    /// <summary>
    /// アニメーションが終わったら呼び出されるコールバック関数を登録する
    /// </summary>
    /// <param name="fadeEndCallback">コールバック関数</param>
    public void Setup(System.Action fadeEndCallback)
    {
        _onFadeComplete = fadeEndCallback;
    }

    /// <summary>
    /// 有効になると設定されたコールバックを呼び出す
    /// </summary>
    private void OnEnable()
    {
        _onFadeComplete?.Invoke();
    }
}
