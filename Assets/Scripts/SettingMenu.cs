using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    private int value = 0;
    public AudioMixer audioMixer;
    public Slider slider;

    private void Start() {
        // update slider master saat scene di load
        if(audioMixer.GetFloat("Master", out float audio))
            {
                slider.value = audio;
            }
    }
    public void SetVolume(float volume)
    {
        // update volume audioMixer
        if(value == 0)
        {
            audioMixer.SetFloat("Master", volume);
        } else if(value == 1)
        {
            audioMixer.SetFloat("Music", volume);
        } else if(value == 2)
        {
            audioMixer.SetFloat("SFX", volume);
        }
    }

    public void setValue(int value)
    {
        this.value = value;

        // Update slider dengan value audioMixer
        if(value == 0)
        {
            if(audioMixer.GetFloat("Master", out float audio))
            {
                slider.value = audio;
            }
        } else if(value == 1)
        {
            if(audioMixer.GetFloat("Music", out float audio))
            {
                slider.value = audio;
            }
        } else if(value == 2)
        {
            if(audioMixer.GetFloat("SFX", out float audio))
            {
                slider.value = audio;
            }
        }
        
    }
}
