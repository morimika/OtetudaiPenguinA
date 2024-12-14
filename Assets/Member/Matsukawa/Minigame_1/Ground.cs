using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ground : MonoBehaviour
{
    public AppleController appleController;

    public bool _goBasket;
    public bool _goBasket2;
    public bool _goBasket3;

    private void Start()
    {
        appleController = GetComponent<AppleController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _goBasket = AppleController._appleGoInBasket;
        _goBasket2 = AppleController._appleGoInBasket2;
        _goBasket3 = AppleController._appleGoInBasket3;

        // guraund(地面にリンゴが触れたとき)
        // おちたリンゴがかごの中に入るアニメーション開始
        if (collision.gameObject.tag == "Apple1")
        {
            _goBasket = true;
        }

        if (collision.gameObject.tag == "Apple2")
        {
            _goBasket2 = true;
        }
        if (collision.gameObject.tag == "Apple3")
        {
            _goBasket3 = true;
        }

    }

}