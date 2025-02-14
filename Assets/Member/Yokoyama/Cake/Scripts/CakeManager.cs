using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CakeManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Fruits;
    [SerializeField] GameObject Tutorial;

    //最大回数
    public int Max_Count;

    private Food food;
    private int Applepie_Count = 0;
    private int Blueberrytart_Count = 0;
    private int Shortcake_Count = 0;
    private bool applepie = false;
    private bool blueberrytart = false;
    private bool shortcake = false;

    public enum Mode
    {
        Anime,
        Game,
    }
    public Mode mode = Mode.Anime;

    //mori
    [SerializeField]
    private GameObject clearCutInObj;
    [SerializeField]
    private TransitonScene _transitonScene;

    private void Update()
    {
        if (HelpManager.IsClear == true && CutInFade.IsFadeFin)
        {
            //クリアしている、かつ、フェードインされて待機中の場合
            if (Input.GetMouseButtonDown(0))
            {
                Invoke(nameof(ReturnGameScene), 1);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("First_FoodMake");
    }

    //[初回]遅らせて、生成
    //※3レーン目だけは、アニメーションの為無し
    IEnumerator First_FoodMake()
    {
        yield return new WaitForSeconds(2.6f);

        Tutorial.SetActive(true);
        Invoke(nameof(GameStart), 8.1f);

        int dice = Random.Range(0, Fruits.Count);

        //1レーン目
        food = Fruits[dice].GetComponent<Food>();
        food.lane = Food.Lane.Lane1;
        Instantiate(Fruits[dice], new Vector3(-6f, -6.25f, 0), Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        dice = Random.Range(0, Fruits.Count);

        //5レーン目
        food = Fruits[dice].GetComponent<Food>();
        food.lane = Food.Lane.Lane5;
        Instantiate(Fruits[dice], new Vector3(6f, -6.25f, 0), Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        dice = Random.Range(0, Fruits.Count);

        //2レーン目
        food = Fruits[dice].GetComponent<Food>();
        food.lane = Food.Lane.Lane2;
        Instantiate(Fruits[dice], new Vector3(-3f, -6.25f, 0), Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        dice = Random.Range(0, Fruits.Count);

        //4レーン目
        food = Fruits[dice].GetComponent<Food>();
        food.lane = Food.Lane.Lane4;
        Instantiate(Fruits[dice], new Vector3(3f, -6.25f, 0), Quaternion.identity);
    }

    private void GameStart()
    {
        Tutorial.SetActive(false);
        mode = Mode.Game;
    }

    //次に生成しなきゃいけないレーン番号を貰って生成
    public void FoodMake(int i)
    {
        //全てのフラグがTrueだったら
        if(applepie && blueberrytart && shortcake)
        {
            //ゲームクリアにする
            //mori
            Instantiate(clearCutInObj);
            PlayerSetPos.PlayerPos = new Vector3(10.5f, -6, 0);
            if (HelpManager.HavingHelpTask == "CakeMake")
            {
                HelpManager.IsClear = true;
            }
            Debug.Log("クリア");
            return;
        }

        int dice = Random.Range(0, Fruits.Count);

        //アップルパイが完成してたら
        if(dice == 0 && applepie)
        {
            FoodMake(i);
            return;
        }
        //ブルーベリーパイが完成してたら
        if(dice == 1 && blueberrytart)
        {
            FoodMake(i);
            return;
        }
        //ショートケーキが完成してたら
        if(dice == 2 && shortcake)
        {
            FoodMake(i);
            return;
        }
        

        food = Fruits[dice].GetComponent<Food>();
        //レーン指定
        switch (i)
        {
            //レーン1
            case 1:
                food.lane = Food.Lane.Lane1;
                Instantiate(Fruits[dice], new Vector3(-6f, -6.25f, 0), Quaternion.identity);
                break;
            //レーン2
            case 2:
                food.lane = Food.Lane.Lane2;
                Instantiate(Fruits[dice], new Vector3(-3f, -6.25f, 0), Quaternion.identity);
                break;
            //レーン3
            case 3:
                food.lane = Food.Lane.Lane3;
                Instantiate(Fruits[dice], new Vector3(0, -6.25f, 0), Quaternion.identity);
                break;
            //レーン4
            case 4:
                food.lane = Food.Lane.Lane4;
                Instantiate(Fruits[dice], new Vector3(3f, -6.25f, 0), Quaternion.identity);
                break;
            //レーン5
            case 5:
                food.lane = Food.Lane.Lane5;
                Instantiate(Fruits[dice], new Vector3(6f, -6.25f, 0), Quaternion.identity);
                break;
        }
    }

    //ケーキ完成までのカウント
    public void PlasCount(string Name)
    {
        switch(Name)
        {
            case "Applepie":
                Applepie_Count++;

                if (Max_Count == Applepie_Count)
                {
                    applepie = true;
                }
                break;
            case "Blueberrytart":
                Blueberrytart_Count++;

                if (Max_Count == Blueberrytart_Count)
                {
                    blueberrytart = true;
                }
                break;
            case "Shortcake":
                Shortcake_Count++;

                if (Max_Count == Shortcake_Count)
                {
                    shortcake = true;
                }
                break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //mori
    //シーン遷移
    public void ReturnGameScene()
    {
        _transitonScene?.LoadScene();
    }
}
