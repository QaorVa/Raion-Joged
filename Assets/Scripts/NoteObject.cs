using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;

    public bool perfect;
    public bool good;
    public bool okay;

    public GameObject okayEffect, goodEffect, perfectEffect, missEffect;
    
    public KeyCode keyToPress;
    public bool hasStarted;
    public float assignedTime;

    double timeInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameIsPaused)
        {
            double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
            float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);
        
            if(Input.GetKeyDown(keyToPress))
            {
                if(canBePressed)
                {
                    if(perfect)
                    {
                        GameManager.instance.PerfectHit();

                        Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);

                    } else if(good)
                    {
                        GameManager.instance.GoodHit();

                        Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);

                    }
                    else if(okay)
                    {
                        GameManager.instance.OkayHit();

                        Instantiate(okayEffect, transform.position, okayEffect.transform.rotation);

                    }

                    gameObject.SetActive(false);

                //GameManager.instance.noteHit();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ngecek kalo panah udah masuk kotak buat dipencet
        if(other.tag == "Perfect")
        {
            perfect = true;
            canBePressed = true;
        }
        if(other.tag == "Good")
        {
            good = true;
            canBePressed = true;
        }
        if(other.tag == "Okay")
        {
            okay = true;
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ngecek kalo panah udah keluar kotak buat dipencet
        if (other.tag == "Perfect" && gameObject.activeSelf)
        {
            perfect = false;
        }
        if (other.tag == "Good" && gameObject.activeSelf)
        {
            good = false;
        }

        if (other.tag == "Okay" && gameObject.activeSelf)
        {
            okay = false;

            canBePressed = false;
            
            GameManager.instance.noteMissed();

            Instantiate(missEffect, transform.position, missEffect.transform.rotation);

        }
    }
}
