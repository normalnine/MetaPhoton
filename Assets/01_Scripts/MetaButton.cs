using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MetaButton : Button
{
    protected override void Start()
    {
        base.Start();
    }
        
    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
        print("OnMove");
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        print("OnPointerDown");
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        print("OnPointerEnter");
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        print("OnPointerExit");
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        print("OnPointerUp");
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        print("OnSelect");
    }
}
