using UnityEngine;

public class MouseClickDetection : MonoBehaviour
{
    private RaycastHit2D hit;
    private Ray ray;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
            if(Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity).collider == null)
            {
                return;
            }
            else
            {
                hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            }
            Debug.Log(hit.collider.gameObject.name);
            //if (hit.collider.gameObject.tag == "NPC")
            //{
            //    Debug.Log(hit.collider.gameObject.tag);
            //}
        }
    }
}
