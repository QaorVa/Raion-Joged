using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float startObject;
    [SerializeField] float moveObject;


    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.moveLocalX(gameObject, moveObject, .4f).setEase(LeanTweenType.easeOutExpo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.moveLocalX(gameObject, startObject, .4f).setEase(LeanTweenType.easeOutExpo);
    }
}
