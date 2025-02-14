using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cake : MonoBehaviour
{
    //ゲームマネージャー
    [SerializeField] CakeManager CKMA;

    [SerializeField] List<GameObject> CakeLine;
    [SerializeField] List<Image> CakeImage;

    private int count;

    private Food food;
    //自分のコライダー
    private BoxCollider2D box2D;

    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //アップルパイの切り株
        if (gameObject.name == "Apple_stump")
        {
            if (collision.gameObject.tag == "Apple")
            {
                switch(count)
                {
                    case 0:
                        CakeImage[0].fillAmount += 0.5f;
                        break;
                    case 1:
                        CakeImage[0].fillAmount += 0.5f;
                        CakeLine[0].SetActive(false);
                        CakeLine[1].SetActive(true);
                        break;
                    case 2:
                        CakeImage[1].fillAmount += 0.5f;
                        break;
                    case 3:
                        CakeImage[1].fillAmount += 0.5f;
                        CakeLine[1].SetActive(false);
                        CakeLine[2].SetActive(true);
                        break;
                    case 4:
                        CakeImage[2].fillAmount += 0.5f;
                        break;
                    case 5:
                        CakeImage[2].fillAmount += 0.5f;
                        CakeLine[2].SetActive(false);
                        box2D.enabled = false;
                        break;
                }
                count++;
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
                switch (count)
                {
                    case 0:
                        CakeImage[0].fillAmount += 0.5f;
                        break;
                    case 1:
                        CakeImage[0].fillAmount += 0.5f;
                        CakeLine[0].SetActive(false);
                        CakeLine[1].SetActive(true);
                        break;
                    case 2:
                        CakeImage[1].fillAmount += 0.5f;
                        break;
                    case 3:
                        CakeImage[1].fillAmount += 0.5f;
                        CakeLine[1].SetActive(false);
                        CakeLine[2].SetActive(true);
                        break;
                    case 4:
                        CakeImage[2].fillAmount += 0.5f;
                        break;
                    case 5:
                        CakeImage[2].fillAmount += 0.5f;
                        CakeLine[2].SetActive(false);
                        box2D.enabled = false;
                        break;
                }
                count++;
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
                switch (count)
                {
                    case 0:
                        CakeImage[0].fillAmount += 0.5f;
                        break;
                    case 1:
                        CakeImage[0].fillAmount += 0.5f;
                        CakeLine[0].SetActive(false);
                        CakeLine[1].SetActive(true);
                        break;
                    case 2:
                        CakeImage[1].fillAmount += 0.5f;
                        break;
                    case 3:
                        CakeImage[1].fillAmount += 0.5f;
                        CakeLine[1].SetActive(false);
                        CakeLine[2].SetActive(true);
                        break;
                    case 4:
                        CakeImage[2].fillAmount += 0.5f;
                        break;
                    case 5:
                        CakeImage[2].fillAmount += 0.5f;
                        CakeLine[2].SetActive(false);
                        box2D.enabled = false;
                        break;
                }
                count++;
                food = collision.gameObject.GetComponent<Food>();
                CKMA.PlasCount("Shortcake");
                Food_RMake();
            }
        }
    }

    //消えた食べ物を情報を渡す
    private void Food_RMake()
    {
        //前いたレーン番号取得
        int count = food.LaneNumber;
        CKMA.FoodMake(count);
        Destroy(food.gameObject);
    }
}
