using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private List<int> _targetNumbers = new List<int>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag is "Target")
        {
            if(int.TryParse(other.name, out int number))
            {
                if(_targetNumbers.Contains(number) is false)
                {
                    _targetNumbers.Add(number);
                    other.GetComponent<GrassView>().SetCheckColor();    
                }

            }
            _targetNumbers.ForEach(num => Debug.Log(num));
        }
    }
}
