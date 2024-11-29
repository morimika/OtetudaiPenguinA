using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Mori Script
public class CupturePic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CaptureCoroutine(500, 500);
        }
    }

    protected virtual IEnumerator CaptureCoroutine(int width, int height)
    {
        // カメラのレンダリング待ち
        yield return new WaitForEndOfFrame();
        Texture2D tex = ScreenCapture.CaptureScreenshotAsTexture();
        // 切り取る画像の左下位置を求める
        int x = (tex.width - width) / 2;
        int y = (tex.height - height) / 2;
        Color[] colors = tex.GetPixels(x, y, width, height);
        Texture2D saveTex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        saveTex.SetPixels(colors);
        File.WriteAllBytes("ss.png", saveTex.EncodeToPNG());
        Debug.Log("HI");
    }
}
