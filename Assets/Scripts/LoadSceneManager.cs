using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f; //lama waktu transisi
    // Start is called before the first frame update
    // pakai method ini pada tombol pindah scene
    private AudioSource bgMenu;

    public void Start()
    {
        gameObject.SetActive(true);
        bgMenu = GameObject.FindWithTag("BGMenu").GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Options")
        {
            bgMenu.Stop();
        }
        else
        {
            if(!bgMenu.isPlaying)
            {
                bgMenu.Play();
            }
            
        }
    }
    public void BackScene()
    {
        // Load scene sebelum scene yang sedang aktif
        StartCoroutine(Scene(SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void LoadScene(string str) //String nama scene yangg dituju
    {
        StartCoroutine(Scene(str));
    }

    IEnumerator Scene(string str)
    {
       transition.SetTrigger("Start");
       yield return new WaitForSecondsRealtime(transitionTime);
       SceneManager.LoadScene(str); 
    }

    IEnumerator Scene(int index)
    {
       transition.SetTrigger("Start");
       yield return new WaitForSecondsRealtime(transitionTime);
       SceneManager.LoadScene(index); 
    }
    public void Exit()
    {
        Application.Quit();
    }
}
