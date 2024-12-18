using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree1 : MonoBehaviour
{
    // ‚è‚ñ‚²‚ª–Ø‚É‚Â‚¢‚Ä‚¢‚é‚Æ‚«‚Í‚»‚Ì–Ø‚É‚è‚ñ‚²‚ğ¶¬‚µ‚È‚¢
    public static bool _blcanGenerateApple1 = false;

    private void Update()
    {
        Debug.Log("1 = " + _blcanGenerateApple1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ‚à‚µ–Ø‚P‚É‚è‚ñ‚²‚P‚ª•t‚¢‚Ä‚¢‚½‚ç–Ø‚P‚É‚è‚ñ‚²‚ğ¶¬‚³‚¹‚È‚¢
        // Untagged‚ª‚Â‚¢‚Ä‚¢‚ê‚Î¶¬‚µ‚Ä‚¢‚¢

        if (collision.gameObject.tag == "Apple1")
        {
            _blcanGenerateApple1 = false;
        }
        else if(collision.gameObject.tag == "Untagged")
        {
            _blcanGenerateApple1 = true;    
        }
    }
}
