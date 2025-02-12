using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovePorti : MonoBehaviour
{
    public GearController GearController; // チェックするスクリプトの参照
    public GearController GearController1; // チェックするスクリプトの参照
    public GearController GearController2; // チェックするスクリプトの参照

    Vector3 UpTransform;//元の位置に戻るときの座標を入れるよう
    Vector3 DownTransform;//下に隠れるときの座標まで行くかの数値を入れるよう

    public float MoveSpeed = 5.0f;
    
    private void Start()
    {
        UpTransform = new Vector3(13.5f, -2.7f, -9);
        DownTransform = new Vector3(13.5f,-7.7f, -9);
    }
    private void Update()
    {
        if (GearController != null && GearController.Set)
        {
            ExecuteAction();
        }
        else if (GearController1 != null && GearController1.Set)
        {
            ExecuteAction();
        }
        else if (GearController2 != null && GearController2.Set)
        {
            ExecuteAction();
        }
        else
        {
            BackPosition();
        }
    }
    void ExecuteAction()
    {
        transform.position =
       Vector3.MoveTowards(transform.position, DownTransform, MoveSpeed);
    }
    void BackPosition()
    {
      
        transform.position =
       Vector3.MoveTowards(transform.position, UpTransform, MoveSpeed);
    }
}
