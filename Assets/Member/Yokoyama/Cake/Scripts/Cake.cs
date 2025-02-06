using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    [SerializeField] GameObject Food_On;
    //次のオブジェクトのコライダー
    [SerializeField] BoxCollider2D Box2D;
    //ゲームマネージャー
    [SerializeField] CakeManager CKMA;

    private Food food;
    //自分のコライダー
    private BoxCollider2D box2D;
    private SpriteRenderer sR;

    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
        sR = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //アップルパイの切り株
        if (gameObject.name == "Apple_stump")
        {
            if (collision.gameObject.tag == "Apple")
            {
                food = collision.gameObject.GetComponent<Food>();
                CKMA.PlasCount("Applepie");
                Food_RMake();
            }
        }
        //アップルパイのデコレーション
        if (gameObject.name == "ApplePie_Decoration")
        {
            if (collision.gameObject.tag == "Apple")
            {
                sR.enabled = false;
                if(Box2D != null)
                {
                    Box2D.enabled = true;
                }
                food = collision.gameObject.GetComponent<Food>();
                CKMA.PlasCount("Applepie");
                Food_RMake();
            }
        }

        //ブルーベリーの切り株
        if (gameObject.name == "BlueberryPie_Stump")
        {
            if (collision.gameObject.tag == "Blueberry")
            {
                food = collision.gameObject.GetComponent<Food>();
                CKMA.PlasCount("Blueberrytart");
                Food_RMake();
            }
        }
        //ブルーベリーパイのデコレーション
        if(gameObject.name == "BlueberryPie_Decoration")
        {
            if (collision.gameObject.tag == "Blueberry")
            {
                sR.enabled = false;
                if (Box2D != null)
                {
                    Box2D.enabled = true;
                }
                food = collision.gameObject.GetComponent<Food>();
                CKMA.PlasCount("Blueberrytart");
                Food_RMake();
            }
        }

        //ストロベリーの切り株
        if (gameObject.name == "StrawberryCake_Stump")
        {
            if (collision.gameObject.tag == "Strawberry")
            {
                food = collision.gameObject.GetComponent<Food>();
                CKMA.PlasCount("Shortcake");
                Food_RMake();
            }
        }
        //ストロベリーケーキのデコレーション
        if (gameObject.name == "StrawberryCake_Decoration")
        {
            if (collision.gameObject.tag == "Strawberry")
            {
                sR.enabled = false;
                if (Box2D != null)
                {
                    Box2D.enabled = true;
                }
                food = collision.gameObject.GetComponent<Food>();
                CKMA.PlasCount("Shortcake");
                Food_RMake();
            }
        }
    }

    //消えた食べ物を情報を渡す
    private void Food_RMake()
    {
        Food_On.SetActive(true);
        box2D.enabled = false;

        //前いたレーン番号取得
        int count = food.LaneNumber;
        CKMA.FoodMake(count);
        Destroy(food.gameObject);
    }
}
