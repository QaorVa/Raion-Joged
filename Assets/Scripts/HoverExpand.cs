using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverExpand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField] float startObject;
    [SerializeField] float scaleObject;

    public bool isSelected;

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject,new Vector3(scaleObject,scaleObject,scaleObject), 0.2f).setEase(LeanTweenType.easeOutExpo);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(startObject, startObject, startObject), 0.2f).setEase(LeanTweenType.easeOutExpo);

        isSelected = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
    }
}
