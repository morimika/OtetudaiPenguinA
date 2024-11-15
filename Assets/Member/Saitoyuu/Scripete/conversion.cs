using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conversion : MonoBehaviour
{
    [SerializeField] GameObject _nextgrass = null;
    private int _count =  0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _nextgrass.SetActive(true);
            this.gameObject.SetActive(false);
            Debug.Log("Player");
            _count++;
        }

    }

}
