using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidSystemSetting : MonoBehaviour
{
    void Start()
    {
        // ¶Œü‚«‚ğ—LŒø‚É‚·‚é
        Screen.autorotateToLandscapeLeft = true;
        // ‰EŒü‚«‚ğ—LŒø‚É‚·‚é
        Screen.autorotateToLandscapeRight = true;

        // ‰æ–Ê‚ÌŒü‚«‚ğ©“®‰ñ“]‚Éİ’è‚·‚é
        Screen.orientation = ScreenOrientation.AutoRotation;

        Input.multiTouchEnabled = false;
    }
}
