using UnityEngine;

public class FollowMaouse : MonoBehaviour
{
    void Update()
    {
        // マウスのスクリーン座標を取得
        Vector3 mousePosition = Input.mousePosition;

        // スクリーン座標をワールド座標に変換
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // オブジェクトのZ座標を調整（平面に置く場合などに必要）
        mousePosition.z = 0;

        // オブジェクトの位置をマウスの位置に設定
        transform.position = mousePosition;
    }
}
