using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodChange : MonoBehaviour
{
    [SerializeField] private List<Transform> _plates = new List<Transform>();
    [SerializeField] private List<GameObject> _friends = new List<GameObject>();
    void Update()
    {
        for (int i = 0; i < _plates.Count; i++)
        {
            if (_plates[i].childCount == 0)
            {
                _friends[i].GetComponent<Animator>().SetTrigger("Normal");
            }
            else if (_plates[i].childCount > 0)
            {
                switch(i)
                {
                    case 0:
                        if (_plates[i].GetChild(i).CompareTag("RedApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Happy");
                        }
                        else if (_plates[i].GetChild(0).CompareTag("GreenApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Sad");
                        }
                        break;
                    case 1:
                        if (_plates[i].GetChild(i).CompareTag("RedApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Sad");
                        }
                        else if (_plates[i].GetChild(0).CompareTag("GreenApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Happy");
                        }
                        break;
                    case 2:
                        if (_plates[i].GetChild(i).CompareTag("RedApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Happy");
                        }
                        else if (_plates[i].GetChild(0).CompareTag("GreenApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Sad");
                        }
                        break;
                    case 3:
                        if (_plates[i].GetChild(i).CompareTag("RedApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Sad");
                        }
                        else if (_plates[i].GetChild(0).CompareTag("GreenApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Happy");
                        }
                        break;
                    case 4:
                        if (_plates[i].GetChild(i).CompareTag("RedApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Happy");
                        }
                        else if (_plates[i].GetChild(0).CompareTag("GreenApple"))
                        {
                            _friends[i].GetComponent<Animator>().SetTrigger("Sad");
                        }
                        break;
                }         
            }
        }
    }
}
