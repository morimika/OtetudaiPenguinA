using UnityEngine;

public class Food : MonoBehaviour
{
    //スピード
    public float Speed;
    //移動停止
    public bool stop = false;
    public int LaneNumber;

    private Drag2 drag2;

    public enum Lane
    {
        Lane1,
        Lane2,
        Lane3,
        Lane4,
        Lane5,
    }
    public Lane lane = Lane.Lane1;

    private void Start()
    {
        drag2 = GetComponent<Drag2>();

        switch (lane)
        {
            case Lane.Lane1:
                LaneNumber = 1;
                break;
            case Lane.Lane2:
                LaneNumber = 2;
                break;
            case Lane.Lane3:
                LaneNumber = 3;
                break;
            case Lane.Lane4:
                LaneNumber = 4;
                break;
            case Lane.Lane5:
                LaneNumber = 5;
                break;
        }
    }

    private void FixedUpdate()
    {
        if(!stop)
        {
            transform.position += new Vector3(0, Speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!drag2.DragOn && stop)
        {
            //マウスが離れた時に戻るポジション(レーン事違う)
            switch (lane)
            {
                case Lane.Lane1:
                    transform.position = new Vector3(-6, -3.05f, 0);
                    break;
                case Lane.Lane2:
                    transform.position = new Vector3(-3, -3.05f, 0);
                    break;
                case Lane.Lane3:
                    transform.position = new Vector3(0, -3.05f, 0);
                    break;
                case Lane.Lane4:
                    transform.position = new Vector3(3, -3.05f, 0);
                    break;
                case Lane.Lane5:
                    transform.position = new Vector3(6, -3.05f, 0);
                    break;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //コンベアの終着点
        if (collision.gameObject.tag == "Finish")
        {
            stop = true;
        }
    }
}
