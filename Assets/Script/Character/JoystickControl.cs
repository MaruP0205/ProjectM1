using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform parentRect;
    public RectTransform knod;
 
    public float range_Move = 100;
    public Vector2 moveDir;

    public void OnBeginDrag(PointerEventData eventData)
    {

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 locol_Pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, null,out locol_Pos);
        moveDir.x = locol_Pos.x /range_Move;
        moveDir.y = locol_Pos.y /range_Move;

        moveDir.x = Mathf.Clamp(moveDir.x, -1, 1);
        moveDir.y = Mathf.Clamp(moveDir.y, -1, 1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        moveDir = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveDir = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputManager.AddMoveDir(new Vector3(moveDir.x, 0, moveDir.y));
        Vector2 moveDir_ = new Vector2(InputManager.move_dir.x, InputManager.move_dir.z);

        if (moveDir_.magnitude >= 1)
        {
            float angle = Mathf.Atan2(moveDir_.y, moveDir_.x);
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            knod.anchoredPosition = new Vector2(x, y) * range_Move;
        }
        else
        {
            knod.anchoredPosition = moveDir_ * range_Move; 
        }
        
        #region Normal Cal
        /* float x = locol_Pos.x / range_Move;
       x = Mathf.Clamp(x,-1,1);
       float y = locol_Pos.y / range_Move;
       y = Mathf.Clamp(y,-1,1);
       moveDir = new Vector2(x, y);
        if (locol_Pos == Vector2.zero )
        {
            Vector3 input_move_dir = InputManager.move_dir;
            Vector2 dir = new Vector2(input_move_dir.x, input_move_dir.z);
            dir = dir * range_Move;
            knod.anchoredPosition = dir;
        }
        else
        {
            Vector2 dir = locol_Pos.normalized;
            dir = dir * range_Move;
            if (locol_Pos.magnitude > dir.magnitude)
                locol_Pos = dir;
            knod.anchoredPosition = locol_Pos;
        }
         InputManager.AddMoveDir(new Vector3(x, 0, y));
         */
        #endregion
    }
}
