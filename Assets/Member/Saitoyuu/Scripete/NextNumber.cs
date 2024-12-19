using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class NextNumber : MonoBehaviour
{
    [SerializeField] GameObject _nextNumberGameObject;
    [SerializeField] GameObject _endNumberGameObject;
    public int _count = 0;
    Grass _grass;

    [SerializeField]
    private GameObject _clearCutInObj;

    //mori
    [SerializeField]
    private ItemData _itemData;
    [SerializeField]
    private ItemList _playerItem;

    private void Start()
    {
        GameObject obj = GameObject.Find("Player");
        _grass = obj.GetComponent<Grass>();

        Debug.Log(this.gameObject.name);
    }

    private void Update()
    {
        _count = _grass._targetNumbers.Count;

        //mori
        //ボタンを押したとき
        if (Input.GetMouseButtonDown(0))
        {
            //クリアしている、かつ、フェードインされて待機中の場合
            if (HelpManager.IsClear && CutInFade.IsFadeFin)
            {
                Invoke(nameof(ReturnGameScene), 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("要素数");
        Debug.Log(_count);
        if ((_count == 10) || (_count == 29) || (_count == 59))
            Invoke(("NextActive"), 2.0f);

        //mori
        if (_count == 108)
        {
            Invoke(nameof(Clear), 1);
        }
    }

    private void NextActive()
    {
        _endNumberGameObject.SetActive(false);
        _nextNumberGameObject.SetActive(true);
        Debug.Log("次の数字");
    }

    //mori
    private void Clear()
    {
        Instantiate(_clearCutInObj);
        HelpManager.IsClear = true;
        PlayerSetPos.PlayerPos = new Vector2(7.1f, 7.88f);
        _playerItem.items.Add(_itemData);
    }

    //mori
    //シーン遷移
    public void ReturnGameScene()
    {
        SceneManager.LoadScene("Mori_MainGameScene");
    }

}
