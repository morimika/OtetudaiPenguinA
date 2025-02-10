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
        UpTransform = new Vector3(-0.3f, 0, -9);
        DownTransform = new Vector3(-0.3f,-7, -9);
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
        /* Vector3 current = transform.position;
         Vector3 target = new Vector3(0, -4, -9);
         float step = MoveActionSpeed; //* Time.deltaTime;
         transform.position = Vector3.MoveTowards(current, target, maxDistanceDelta);*/
        transform.position =
       Vector3.MoveTowards(transform.position, DownTransform, MoveSpeed);
    }
    void BackPosition()
    {
        /* Vector3 current = transform.position;
         Vector3 target = new Vector3(0, 0, -9);
         float step = MoveActionSpeed;// * Time.deltaTime;
         transform.position = Vector3.MoveTowards(current, target, maxDistanceDelta);*/
        transform.position =
       Vector3.MoveTowards(transform.position, UpTransform, MoveSpeed);
    }
}
