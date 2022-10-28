using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickMove : MonoBehaviour
{
    public GameObject masterPos;
    public GameObject musicPos;
    public GameObject SFXPos;

    [SerializeField] float startObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onMasterClick()
    {
        LeanTween.moveX(gameObject, masterPos.transform.position.x, .3f).setEase(LeanTweenType.easeOutExpo);
    }

    public void onMusicClick()
    {
        LeanTween.moveX(gameObject, musicPos.transform.position.x, .3f).setEase(LeanTweenType.easeOutExpo);
    }

    public void onSFXClick()
    {
        LeanTween.moveX(gameObject, SFXPos.transform.position.x, .3f).setEase(LeanTweenType.easeOutExpo);
    }

    public void onClickScale()
    {
        LeanTween.scale(gameObject, new Vector3(startObject, startObject, startObject), 0.2f).setEase(LeanTweenType.easeOutExpo);
    }
}
