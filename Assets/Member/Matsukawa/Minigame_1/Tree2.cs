using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree2 : MonoBehaviour
{
    // ‚è‚ñ‚²‚ª–Ø‚É‚Â‚¢‚Ä‚¢‚é‚Æ‚«‚Í‚»‚Ì–Ø‚É‚è‚ñ‚²‚ğ¶¬‚µ‚È‚¢
    public static bool _blcanGenerateApple2 = false;

    private void Update()
    {
        Debug.Log("2 = " + _blcanGenerateApple2);

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // ‚à‚µ–Ø‚P‚É‚è‚ñ‚²‚P‚ª•t‚¢‚Ä‚¢‚½‚ç–Ø‚P‚É‚è‚ñ‚²‚ğ¶¬‚³‚¹‚È‚¢
        // Untagged‚ª‚Â‚¢‚Ä‚¢‚ê‚Î¶¬‚µ‚Ä‚¢‚¢

        if (collision.gameObject.tag == "Apple2" )
        {
            _blcanGenerateApple2 = false;
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            _blcanGenerateApple2 = true;
        }
    }
}
