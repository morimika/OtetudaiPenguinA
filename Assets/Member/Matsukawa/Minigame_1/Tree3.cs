using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree3 : MonoBehaviour
{
    // ‚è‚ñ‚²‚ª–Ø‚É‚Â‚¢‚Ä‚¢‚é‚Æ‚«‚Í‚»‚Ì–Ø‚É‚è‚ñ‚²‚ğ¶¬‚µ‚È‚¢
    public static bool _blcanGenerateApple3 = false;

    private void Update()
    {
        Debug.Log("3 = " + _blcanGenerateApple3);

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // ‚à‚µ–Ø‚P‚É‚è‚ñ‚²‚P‚ª•t‚¢‚Ä‚¢‚½‚ç–Ø‚P‚É‚è‚ñ‚²‚ğ¶¬‚³‚¹‚È‚¢
        // Untagged‚ª‚Â‚¢‚Ä‚¢‚ê‚Î¶¬‚µ‚Ä‚¢‚¢

        if (collision.gameObject.tag == "Apple3" )
        {
            _blcanGenerateApple3 = false;
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            _blcanGenerateApple3 = true;
        }
    }
}
