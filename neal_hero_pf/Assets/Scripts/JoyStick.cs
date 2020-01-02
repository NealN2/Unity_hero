using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    public static JoyStick Instance  //singleton
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<JoyStick>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject("JoyStick");
                    instance = instanceContainer.AddComponent<JoyStick>();
                }
            }
            return instance;
        }
    }

    private static JoyStick instance;

    public GameObject smallStick;
    public GameObject bGStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVec;
    float stickRadius;
    Vector3 joyStickFirstPosition;
    public bool isPlayerMoving = false;

    void Start()
    {
        stickRadius = bGStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        joyStickFirstPosition = bGStick.transform.position;
    }

    
    public void PointDown()
    {
        isPlayerMoving = true;

        bGStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPosition = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        

        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        joyVec = (DragPosition - stickFirstPosition).normalized;

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition);

        if (stickDistance < stickRadius)
            smallStick.transform.position = stickFirstPosition + joyVec * stickDistance;
        else
            smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
    } 

    public void Drop()
    {
        joyVec = Vector3.zero;
        bGStick.transform.position = joyStickFirstPosition;
        smallStick.transform.position = joyStickFirstPosition;

        isPlayerMoving = false;
    }
}
