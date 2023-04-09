using UnityEngine;
using UnityEngine.EventSystems;
public class U_Handshank : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public UISystem uisystem;
    private RectTransform Handle;
    private bool IsPressed;//是否按下
    private float MaxDistance;//手柄移动最大距离（默认为两个UI半径的差）
    private Vector2 OutputVector = Vector2.zero;//手柄输出向量
    private float DistanceFromCenter = 0;//手柄距离中心距离
    private float HandleAngle = 0;//手柄旋转角度
    void Start()
    {
        Handle = transform.parent.Find("Handle").GetComponent<RectTransform>();
        MaxDistance = (transform.GetComponent<RectTransform>().rect.width - Handle.rect.width) * GameObject.Find("UI/BattleUI").GetComponent<RectTransform>().localScale.x / 2;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (IsPressed)
        {
            Vector2 fingerPosition = eventData.position;
            float dis = Vector2.Distance(fingerPosition, transform.position);//手柄原点到手指位置
            float rate = dis / MaxDistance;//手指位置超了规定几倍
            if (rate > 1)
            {
                fingerPosition = new Vector2(((fingerPosition.x - transform.position.x) / rate) + transform.position.x, ((fingerPosition.y - transform.position.y) / rate) + transform.position.y);//除回去
            }
            Handle.position = fingerPosition;
            Vector2 direction = new Vector2(fingerPosition.x - transform.position.x, fingerPosition.y - transform.position.y);//计算朝着手指向量
            HandleAngle = Vector2.SignedAngle(transform.position, direction) - 45;
            Handle.eulerAngles = new Vector3(0, 0, HandleAngle);//计算角度
            OutputVector = direction / MaxDistance;//把direction范围从0-MaxDistance变成0-1
            DistanceFromCenter = Vector2.Distance(Vector2.zero, OutputVector);
            SendDataToUIsystem();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        //全部归零
        Handle.position = transform.position;
        Handle.eulerAngles = new Vector3(0, 0, 0);
        OutputVector = Vector2.zero;
        DistanceFromCenter = 0;
        SendDataToUIsystem();
    }
    private void SendDataToUIsystem()
    {
        uisystem.HandleDirection = OutputVector;
        uisystem.HandleDistance = DistanceFromCenter;
        uisystem.HandleAngle = HandleAngle;
    }
}
