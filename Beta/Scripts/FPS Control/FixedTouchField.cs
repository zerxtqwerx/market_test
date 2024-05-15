using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Vector2 TouchDist;
    [SerializeField] private Vector2 PointerOld;
    private int PointerId;
    private bool Pressed;
    public UnityEvent OnPress;
    public UnityEvent OnPressEnd;

    public Vector2 GetTouchDist()
    {
        return TouchDist;
    }

    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
        OnPress?.Invoke();
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        OnPressEnd?.Invoke();
    }
    
}