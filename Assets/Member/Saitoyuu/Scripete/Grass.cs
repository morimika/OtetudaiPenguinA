using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
   [SerializeField] public List<int> _targetNumbers = new List<int>();
    [SerializeField]private int Counter = 0;

    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag is "Target")
        {
            if(int.TryParse(other.name, out int number))
            {
                if(number == _targetNumbers.Count)
                {
                    if (_targetNumbers.Contains(number) is false)
                    {
                        _targetNumbers.Add(number);
                        other.GetComponent<GrassView>().SetCheckColor();
                    }
                }
            }
           // Debug.Log("----------");
            _targetNumbers.ForEach(num => Debug.Log(num));
        }
    }
}
