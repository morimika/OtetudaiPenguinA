using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class NextNumber : MonoBehaviour
{
    [SerializeField] GameObject _nextNumberGameObject;
    [SerializeField] GameObject _endNumberGameObject;
    [SerializeField] GameObject _nextUINumberGameObject;
    [SerializeField] GameObject _endUINumberGameObject;
    public int _count = 0;
    Grass _grass;

    //mori
    [SerializeField]
    private GameObject _clearCutInObj;
    [SerializeField]
    private TransitonScene _transitonScene;

    private void Start()
    {
        GameObject obj = GameObject.Find("Player");
        _grass = obj.GetComponent<Grass>();
        _transitonScene = GetComponent<TransitonScene>();

        Debug.Log(this.gameObject.name);
    }

    private void Update()
    {
        _count = _grass._targetNumbers.Count;

        //mori
        //�{�^�����������Ƃ�
        if (Input.GetMouseButtonDown(0))
        {
            //�N���A���Ă���A���A�t�F�[�h�C������đҋ@���̏ꍇ
            if (HelpManager.IsClear == true && CutInFade.IsFadeFin)
            {
                Invoke(nameof(ReturnGameScene), 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�v�f��");
        Debug.Log(_count);
        if ((_count == 10) || (_count == 29) || (_count == 58))
            Invoke(("NextActive"), 2.0f);

        //mori
        if (_count == 107)
        {
            Invoke(nameof(Clear), 1);
        }
    }

    private void NextActive()
    {
        _endNumberGameObject.SetActive(false);
        _endUINumberGameObject.SetActive(false);
        _nextNumberGameObject.SetActive(true);
        _nextUINumberGameObject.SetActive(true);
        Debug.Log("���̐���");
    }

    //mori
    private void Clear()
    {
        Instantiate(_clearCutInObj);
        if(HelpManager.HavingHelpTask== "KanjiHarvest")
        {
            HelpManager.IsClear = true;
        }
        PlayerSetPos.PlayerPos = new Vector2(6.5f, 9f);
    }

    //mori
    //�V�[���J��
    public void ReturnGameScene()
    {
        _transitonScene?.LoadScene();
    }

}
