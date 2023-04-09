using UnityEngine;
using UnityEngine.EventSystems;
public class U_Handshank : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public UISystem uisystem;
    private RectTransform Handle;
    private bool IsPressed;//�Ƿ���
    private float MaxDistance;//�ֱ��ƶ������루Ĭ��Ϊ����UI�뾶�Ĳ
    private Vector2 OutputVector = Vector2.zero;//�ֱ��������
    private float DistanceFromCenter = 0;//�ֱ��������ľ���
    private float HandleAngle = 0;//�ֱ���ת�Ƕ�
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
            float dis = Vector2.Distance(fingerPosition, transform.position);//�ֱ�ԭ�㵽��ָλ��
            float rate = dis / MaxDistance;//��ָλ�ó��˹涨����
            if (rate > 1)
            {
                fingerPosition = new Vector2(((fingerPosition.x - transform.position.x) / rate) + transform.position.x, ((fingerPosition.y - transform.position.y) / rate) + transform.position.y);//����ȥ
            }
            Handle.position = fingerPosition;
            Vector2 direction = new Vector2(fingerPosition.x - transform.position.x, fingerPosition.y - transform.position.y);//���㳯����ָ����
            HandleAngle = Vector2.SignedAngle(transform.position, direction) - 45;
            Handle.eulerAngles = new Vector3(0, 0, HandleAngle);//����Ƕ�
            OutputVector = direction / MaxDistance;//��direction��Χ��0-MaxDistance���0-1
            DistanceFromCenter = Vector2.Distance(Vector2.zero, OutputVector);
            SendDataToUIsystem();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        //ȫ������
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
