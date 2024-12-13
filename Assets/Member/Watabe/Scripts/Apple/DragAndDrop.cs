using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // メインカメラを取得
    }

    void Update()
    {
        // マウスやタッチ入力をチェック
        if (Input.GetMouseButtonDown(0)) // マウスの左クリックまたはタッチ開始
        {
            Vector3 mousePos = GetWorldPosition(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // オブジェクトに当たった場合、ドラッグを開始
                isDragging = true;
                offset = transform.position - mousePos;
            }
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0)) // マウスの左クリックまたはタッチ中
            {
                // マウスやタッチ位置に基づいてオブジェクトの位置を更新
                Vector3 mousePos = GetWorldPosition(Input.mousePosition);
                transform.position = mousePos + offset;
            }

            if (Input.GetMouseButtonUp(0)) // マウスの左クリックを離すまたはタッチを離す
            {
                // ドラッグを終了
                isDragging = false;
            }
        }
    }

    // スクリーン座標をワールド座標に変換するヘルパーメソッド
    Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0; // 2Dのためz座標は固定
        return worldPosition;
    }
}
