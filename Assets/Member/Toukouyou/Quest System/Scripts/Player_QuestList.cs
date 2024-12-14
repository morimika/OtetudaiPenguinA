using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_QuestList : MonoBehaviour
{
    public static Player_QuestList instance;
    public List<QuestDetail> questList = new List<QuestDetail>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}
