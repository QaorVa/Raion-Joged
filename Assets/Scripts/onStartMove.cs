using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onStartMove : MonoBehaviour
{
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.active && !isActive)
        {
            isActive = true;
            LeanTween.moveLocalY(gameObject, 0f, 1f)
                .setDelay(0.5f)
                .setIgnoreTimeScale(true)
                .setEase(LeanTweenType.easeOutBounce);
        }
    }
}
