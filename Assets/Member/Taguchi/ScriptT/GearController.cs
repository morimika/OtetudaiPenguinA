using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;


public class GearController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

//    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;//ギアに答えの数字を入れるために使う

    private int _gireIndex;
    private System.Action<int> _hitCallback;
    /// ////////////////////////////////////////////////
    [SerializeField] private int _index;
    public static bool NowSet = false;

    [SerializeField]public GameObject TargetPoint;//ギアをポーチの所に戻すためのターゲット
    [SerializeField] public GameObject ClockTargetPoint;//ギアをポーチの所に戻すためのターゲット

    Transform GearPointTr; 
    public float speed = 15.0f;//ギアを手から離したときにポーチに帰る速度
    public bool Set = false;
    private float SetSpeed;

    [SerializeField] public GameObject MoveCamera;//カメラを移動させる時に使うよう
    [SerializeField] public GameObject NextButton;

    public GameObject Porti;//ポーチを入れるよう
    private void Start()
    {
        GearPointTr = TargetPoint.transform;
        SetSpeed = speed;
        //自分の持っているインデックスによって色を変える
        /*
        if(_index == 1)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (_index == 2)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (_index == 3)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        */


    }

    #region ドラッグ処理

    private bool               _isDragging = false;
    private Vector3            _diffPosition;
    private bool               _isAttach = false;
    private System.Action<int> _callback;
    private bool               _isEnable = true;

    public void Setup(System.Action<int> callback)
    {
        _callback = callback;
    }

    public void SetGearEnable(bool enable)
    {
        _isEnable = enable;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isEnable == false) return;
        _isDragging = true;
        var startPos = Camera.main.ScreenToWorldPoint(eventData.position);
        _diffPosition = startPos - transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isEnable == false) return;
        if (_isDragging)
        {
            var tapPos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = tapPos - _diffPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }

#endregion

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TargetCenter") && _isDragging == false && _isAttach == false)
        {
            _isAttach = true;
            NowSet = true;
            transform.DOMove(other.transform.position, 0.2f).OnComplete(() => _callback?.Invoke(_index));
            Set = true;
            NextButton.SetActive(true);//判定をするための次に移動するボタンを表示する 
            this.gameObject.transform.parent = null;//親子解除
            _hitCallback?.Invoke(_gireIndex);

            Vector3 toDirection = ClockTargetPoint.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(GearPointTr.position.x, GearPointTr.position.y),
                speed * Time.deltaTime);
            //接触している間回転する
            transform.Rotate(new Vector3(0, 0, 5));
        }
        else
        {
            NowSet = false;
            
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isAttach = false;

        if (other.gameObject.CompareTag("TargetCenter"))//歯車を時計から外したときに呼び出される
        {
            Set = false;
            NextButton.SetActive(false);
            this.gameObject.transform.parent = Porti.gameObject.transform;//GameObject.Find("Porti").transform;
        }
        
    }

    private void Update()
    {
        if (Set == false)
        {
            speed = SetSpeed;
            Vector3 toDirection = TargetPoint.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(GearPointTr.position.x, GearPointTr.position.y),
                speed * Time.deltaTime);
        }
        else if (Set == true)
        {
            speed = 0;
        }
    }
    public void Setup(int gireIndex, int answerNum, System.Action<int> hitCallback)
    {
       // _textMeshProUGUI.text = answerNum.ToString();
        _gireIndex = gireIndex;
        _hitCallback = hitCallback;
    }
}
