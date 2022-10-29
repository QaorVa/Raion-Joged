using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour, IPointerEnterHandler
{

    public bool hoverSFX = true;

    public bool clickSFX = true;
    public bool nextClickSFX;
    public bool playClickSFX;
    public bool backClickSFX;
    public bool pauseClickSFX;
    public bool masterClickSFX;

    private AudioSource sfxHover;
    private AudioSource sfxClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(hoverSFX)
        {
            sfxHover.Play();
        }
    }

    void Start () {
		GetComponent<Button>().onClick.AddListener(TaskOnClick);
        if(hoverSFX)
        {
            sfxHover = GameObject.FindWithTag("ButtonHover").GetComponent<AudioSource>();
        }
        if(clickSFX)
        {
            sfxClick = GameObject.FindWithTag("ButtonClick").GetComponent<AudioSource>();
        }
        if(nextClickSFX)
        {
            sfxClick = GameObject.FindWithTag("NextButtonClick").GetComponent<AudioSource>();
        }
        if(playClickSFX)
        {
            sfxClick = GameObject.FindWithTag("PlayButtonClick").GetComponent<AudioSource>();
        }
        if(backClickSFX)
        {
            sfxClick = GameObject.FindWithTag("BackButtonClick").GetComponent<AudioSource>();
        }
        if(pauseClickSFX)
        {
            sfxClick = GameObject.FindWithTag("PauseClick").GetComponent<AudioSource>();
        }
        if(masterClickSFX)
        {
            sfxClick = GameObject.FindWithTag("MasterClick").GetComponent<AudioSource>();
        }
	}

	void TaskOnClick(){
        
		if(sfxClick!=null)
        {
            sfxClick.Play();
        }
	}
}
