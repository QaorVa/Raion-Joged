using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(gameObject, new Vector3(0f, 0f, 360f), -360f, 20f).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
