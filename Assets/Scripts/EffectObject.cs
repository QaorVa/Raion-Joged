using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime = 1;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), 0f);

        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.5f).setEase(LeanTweenType.punch);

        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), 0.5f)
            .setDelay(0.5f)
            .setEase(LeanTweenType.easeOutExpo);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
